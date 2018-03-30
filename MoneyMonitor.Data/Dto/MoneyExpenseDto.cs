namespace MoneyMonitor.Data.Dto
{
    public class MoneyExpenseDto
    {
        public string NameExpense { get; set; } = string.Empty;
        public ExpenseTypes TypeExpense { get; set; }
        public double ValueExpense { get; set; }
    }

    public enum ExpenseTypes
    {
        Fixed,
        Variable,
        Charity
    }
}
