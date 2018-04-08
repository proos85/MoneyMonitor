using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModernHttpClient;
using MoneyMonitor.Authentication;
using MoneyMonitor.Client.Base;
using MoneyMonitor.Data.Dto;
using MoneyMonitor.LocalStorage;
using MoneyMonitor.ViewModel;
using Newtonsoft.Json;
using SQLite;

namespace MoneyMonitor.Client.Overview
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OverviewClient : BaseClient, IOverviewClient
    {
        private readonly ILocalSqlLiteConnectionPath _localSqlLiteConnectionPath;
        private readonly IB2CAuthenticationProvider _authenticationProvider;

        public OverviewClient(
            ILocalSqlLiteConnectionPath localSqlLiteConnectionPath,
            IB2CAuthenticationProvider authenticationProvider)
        {
            _localSqlLiteConnectionPath = localSqlLiteConnectionPath;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<IList<MoneyExpenseViewModel>> LoadLocalMoneyExpensesAsync()
        {
            var localData = await LoadOverviewFromLocalAsync().ConfigureAwait(false);
            var vm = ConvertToViewModel(localData);
            return vm;
        }

        public async Task<IList<MoneyExpenseViewModel>> RetrieveRemoteAndSyncWithLocalOne()
        {
            var localData = await LoadOverviewFromLocalAsync().ConfigureAwait(false);
            var remoteData = await RetrieveExpensesRemoteAsync().ConfigureAwait(false);

            await SyncModelsAsync(localData, remoteData).ConfigureAwait(false);
            var vm = ConvertToViewModel(await LoadOverviewFromLocalAsync().ConfigureAwait(false));
            return vm;
        }

        async Task SyncModelsAsync(
            List<MoneyExpenseDto> localData, 
            List<MoneyExpenseDto> remoteData)
        {
            var newData = new List<MoneyExpenseDto>();
            var changedData = new List<MoneyExpenseDto>();
            var deletedData = new List<MoneyExpenseDto>();
            
            // newData
            foreach (var remote in remoteData)
            {
                bool existsInLocal = localData.Any(l => l.Id.Equals(remote.Id, StringComparison.InvariantCultureIgnoreCase));
                if (!existsInLocal)
                {
                    newData.Add(remote);
                }
            }
            await InsertNewDataToLocalStorageAsync(newData);


            // changedData
            changedData.AddRange(
                localData
                    .Join(remoteData, l => l.Id, r => r.Id, (l, r) => new { Local = l, Remote = r })
                    .Where(x =>
                    {
                        bool doesNameMatch = x.Local.NameExpense.Equals(x.Remote.NameExpense, StringComparison.InvariantCultureIgnoreCase);
                        bool doesValueMatch = x.Local.ValueExpense == x.Remote.ValueExpense;
                        bool doesTypeMatch = x.Local.TypeExpense == x.Remote.TypeExpense;

                        return !doesNameMatch || !doesValueMatch || !doesTypeMatch;
                    })
                    .Select(x => x.Remote)
            );
            await UpdateDataInLocalStorageAsync(changedData);


            // deletedData
            foreach (var local in localData)
            {
                bool existsInRemote = remoteData.Any(r => r.Id.Equals(local.Id, StringComparison.InvariantCultureIgnoreCase));
                if (!existsInRemote)
                {
                    deletedData.Add(local);
                }
            }
            await DeleteDataFromLocalStorageAsync(deletedData);
        }

        async Task DeleteDataFromLocalStorageAsync(List<MoneyExpenseDto> deletedData)
        {
            if (deletedData.Any())
            {
                var conn = new SQLiteAsyncConnection(_localSqlLiteConnectionPath.LocalConnection);
                await conn.CreateTableAsync<MoneyExpenseDto>().ConfigureAwait(false);

                foreach (var entity in deletedData)
                {
                    await conn.DeleteAsync(entity).ConfigureAwait(false);
                }
            }
        }

        async Task UpdateDataInLocalStorageAsync(List<MoneyExpenseDto> changedData)
        {
            if (changedData.Any())
            {
                var conn = new SQLiteAsyncConnection(_localSqlLiteConnectionPath.LocalConnection);
                await conn.CreateTableAsync<MoneyExpenseDto>().ConfigureAwait(false);

                foreach (var entity in changedData)
                {
                    await conn.UpdateAsync(entity).ConfigureAwait(false);
                }
            }
        }

        async Task InsertNewDataToLocalStorageAsync(List<MoneyExpenseDto> newData)
        {
            if (newData.Any())
            {
                var conn = new SQLiteAsyncConnection(_localSqlLiteConnectionPath.LocalConnection);
                await conn.CreateTableAsync<MoneyExpenseDto>().ConfigureAwait(false);
            
                foreach (var entity in newData)
                {
                    await conn.InsertAsync(entity).ConfigureAwait(false);
                }
            }
        }

        List<MoneyExpenseViewModel> ConvertToViewModel(List<MoneyExpenseDto> dto)
        {
            return dto
                .Select(x => new MoneyExpenseViewModel
                {
                    NameExpense = x.NameExpense,
                    ValueExpense = x.ValueExpense,
                    TypeExpense = x.TypeExpense
                })
                .ToList();
        }

        Task<List<MoneyExpenseDto>> LoadOverviewFromLocalAsync()
        {
            return Task.Run(async () =>
            {
                var conn = new SQLiteAsyncConnection(_localSqlLiteConnectionPath.LocalConnection);
                await conn.CreateTableAsync<MoneyExpenseDto>().ConfigureAwait(false);
                var result = await conn.Table<MoneyExpenseDto>().ToListAsync().ConfigureAwait(false);
                return result;
            });
        }

        private Task<List<MoneyExpenseDto>> RetrieveExpensesRemoteAsync()
        {
            return Task.Run(async () =>
            {
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    client.BaseAddress = BaseUrl;

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _authenticationProvider.AuthenticationResult.AccessToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                        "api/GetExpenses?code=u5ozYH9TTsCTmXwod5UjP4IALqDaZaJuB2b9RonvxTbCzD28reLvaA==");
                    var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var dto = JsonConvert.DeserializeObject<IEnumerable<MoneyExpenseDto>>(responseContent).ToList();
                    return dto;
                }
            });
        }
    }
}