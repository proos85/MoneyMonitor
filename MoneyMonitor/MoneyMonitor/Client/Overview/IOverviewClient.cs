using System.Collections.Generic;

namespace MoneyMonitor.Client.Overview
{
    public interface IOverviewClient
    {
        IList<string> LoadMoneyExpenses();
    }

    public class MockOverviewClient: IOverviewClient
    {
        public IList<string> LoadMoneyExpenses()
        {
            return new List<string>{"1", "2"};
        }
    }
}