using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace MoneyMonitor.ViewModel
{
    public class MoneyExpenseViewModel: BaseViewModel
    {
        public string NameExpense { get; set; }
        public string TypeExpense { get; set; }

        public double ValueExpense { get; set; }
        public string ValueExpenseString => ValueExpense.ToString("C", new CultureInfo("nl-NL"));
    }
}