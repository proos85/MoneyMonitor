using SQLite;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace MoneyMonitor.Data.Dto
{
    [Table("MoneyExpense")]
    public class MoneyExpenseDto
    {
        [PrimaryKey]
        public string Id { get; set; }
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
