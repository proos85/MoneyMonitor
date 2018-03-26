namespace MoneyMonitor.ViewModel
{
    public enum ExpenseTypes
    {
        Fixed,
        Variable,
        Charity
    }

    public class MoneyExpenseViewModel: BaseViewModel
    {
        public string NameExpense { get; set; }
        public ExpenseTypes TypeExpense { get; set; }
        public double ValueExpense { get; set; }

        public string ValueExpenseString => ValueExpense.ToString("C");
    }
}