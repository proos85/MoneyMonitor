using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MoneyMonitor.Data.Dto;
using Xamarin.Forms;

namespace MoneyMonitor.ViewModel
{
    public class OverviewViewModel: BaseViewModel
    {
        private ObservableCollection<MoneyExpenseViewModel> _moneyExpenses = new ObservableCollection<MoneyExpenseViewModel>();
        public ObservableCollection<MoneyExpenseViewModel> MoneyExpenses
        {
            get => _moneyExpenses;
            set
            {
                if (_moneyExpenses != value)
                {
                    _moneyExpenses = value;
                    OnPropertyChanged(nameof(MoneyExpenses));
                }
            }
        }

        private string _sumMoneyTotalExpenses = 0.ToString("C2");
        public string SumMoneyTotalExpenses
        {
            get => _sumMoneyTotalExpenses;
            set
            {
                if (_sumMoneyTotalExpenses != value)
                {
                    _sumMoneyTotalExpenses = value;
                    OnPropertyChanged(nameof(SumMoneyTotalExpenses));
                }
            }
        }

        private string _sumMoneyFixedExpenses = 0.ToString("C2");
        public string SumMoneyFixedExpenses
        {
            get => _sumMoneyFixedExpenses;
            set
            {
                if (_sumMoneyFixedExpenses != value)
                {
                    _sumMoneyFixedExpenses = value;
                    OnPropertyChanged(nameof(SumMoneyFixedExpenses));
                }
            }
        }

        private string _sumMoneyVariableExpenses = 0.ToString("C2");
        public string SumMoneyVariableExpenses
        {
            get => _sumMoneyVariableExpenses;
            set
            {
                if (_sumMoneyVariableExpenses != value)
                {
                    _sumMoneyVariableExpenses = value;
                    OnPropertyChanged(nameof(SumMoneyVariableExpenses));
                }
            }
        }

        private string _sumMoneyChiarityExpenses = 0.ToString("C2");
        public string SumMoneyChiarityExpenses
        {
            get => _sumMoneyChiarityExpenses;
            set
            {
                if (_sumMoneyChiarityExpenses != value)
                {
                    _sumMoneyChiarityExpenses = value;
                    OnPropertyChanged(nameof(SumMoneyChiarityExpenses));
                }
            }
        }

        public ICommand RefreshCommand => new Command(RefreshCommandHandler);

        public OverviewViewModel()
        {
            MoneyExpenses.CollectionChanged += MoneyExpensesOnCollectionChanged;
        }

        private void MoneyExpensesOnCollectionChanged(
            object sender, 
            NotifyCollectionChangedEventArgs args)
        {
            CalculateExpensesSum();
        }

        private void CalculateExpensesSum()
        {
            SumMoneyTotalExpenses = ConvertToCurrency(MoneyExpenses.Sum(x => x.ValueExpense));
            SumMoneyFixedExpenses = ConvertToCurrency(MoneyExpenses.Where(x => x.TypeExpense == ExpenseTypes.Fixed).Sum(x => x.ValueExpense));
            SumMoneyVariableExpenses = ConvertToCurrency(MoneyExpenses.Where(x => x.TypeExpense == ExpenseTypes.Variable).Sum(x => x.ValueExpense));
            SumMoneyChiarityExpenses = ConvertToCurrency(MoneyExpenses.Where(x => x.TypeExpense == ExpenseTypes.Charity).Sum(x => x.ValueExpense));
        }

        private string ConvertToCurrency(double value)
        {
            return value.ToString("C2");
        }

        private async void RefreshCommandHandler()
        {
            await Task.Delay(1000);

            // Todo: refresh data

            MessagingCenter.Send<object>(this, "RefreshingComplete");
        }
    }
}