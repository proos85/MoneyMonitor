//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using MoneyMonitor.Data.Dto;
//using MoneyMonitor.ViewModel;

//namespace MoneyMonitor.Client.Overview.Mock
//{
//    // ReSharper disable once ClassNeverInstantiated.Global
//    public class MockOverviewClient: IOverviewClient
//    {
//        public async Task<IList<MoneyExpenseViewModel>> LoadLocalMoneyExpensesAsync()
//        {
//            await Task.Delay(500).ConfigureAwait(false);

//            var result = new List<MoneyExpenseViewModel>();

//            var rnd = new Random();
//            int numExpenses = rnd.Next(0, 100);
//            for (int i = 0; i < numExpenses; i++)
//            {
//                result.Add(new MoneyExpenseViewModel
//                {
//                    NameExpense = $"Uitgave {i+1}",
//                    TypeExpense = GetExpenseType(rnd),
//                    ValueExpense = rnd.Next(0, 500)
//                });
//            }

//            return result;
//        }

//        public Task<IList<MoneyExpenseViewModel>> LoadRemoteMoneyExpensesAsync()
//        {
//            throw new NotImplementedException();
//        }

//        private ExpenseTypes GetExpenseType(Random rnd)
//        {
//            var nextTypeId = rnd.Next(0, 5000);

//            if (nextTypeId < 2000)
//                return ExpenseTypes.Fixed;

//            if (nextTypeId < 4000)
//                return ExpenseTypes.Variable;

//            return ExpenseTypes.Charity;
//        }
//    }
//}