using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyMonitor.ViewModel;

namespace MoneyMonitor.Client.Overview
{
    public interface IOverviewClient
    {
        Task<IList<MoneyExpenseViewModel>> LoadLocalMoneyExpensesAsync();
        Task<IList<MoneyExpenseViewModel>> RetrieveRemoteAndSyncWithLocalOne();
    }
}