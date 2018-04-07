using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModernHttpClient;
using MoneyMonitor.Authentication;
using MoneyMonitor.Client.Base;
using MoneyMonitor.Data.Dto;
using MoneyMonitor.ViewModel;
using Newtonsoft.Json;

namespace MoneyMonitor.Client.Overview
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OverviewClient : BaseClient, IOverviewClient
    {
        private readonly IB2CAuthenticationProvider _authenticationProvider;

        public OverviewClient(IB2CAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }

        async Task<IList<MoneyExpenseViewModel>> IOverviewClient.LoadMoneyExpensesAsync()
        {
            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                client.BaseAddress = BaseUrl;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationProvider.AuthenticationResult.AccessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/GetExpenses?code=u5ozYH9TTsCTmXwod5UjP4IALqDaZaJuB2b9RonvxTbCzD28reLvaA==");
                var response = await client.SendAsync(requestMessage).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var dto = JsonConvert.DeserializeObject<IEnumerable<MoneyExpenseDto>>(responseContent);
               
                var vm = new List<MoneyExpenseViewModel>();
                vm.AddRange(dto
                    .Select(x => new MoneyExpenseViewModel
                    {
                        NameExpense = x.NameExpense,
                        ValueExpense = x.ValueExpense,
                        TypeExpense = x.TypeExpense
                    }));

                return vm;
            }
        }
    }
}