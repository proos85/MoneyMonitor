using MoneyMonitor.Data.Dto;
using MoneyMonitor.ViewModel.Base;

namespace MoneyMonitor.ViewModel
{
    public class MoneyExpenseViewModel: BaseViewModel
    {
        public string NameExpense { get; set; }
        public ExpenseTypes TypeExpense { get; set; }
        public double ValueExpense { get; set; }

        public string ValueExpenseString => ValueExpense.ToString("C");
    }
}