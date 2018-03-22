using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private string _sumMoneyExpenses = 0.ToString("C2", new CultureInfo("nl-NL"));
        public string SumMoneyExpenses
        {
            get => _sumMoneyExpenses;
            set
            {
                if (_sumMoneyExpenses != value)
                {
                    _sumMoneyExpenses = value;
                    OnPropertyChanged(nameof(SumMoneyExpenses));
                }
            }
        }

        public ICommand RefreshCommand { get; set; }

        public OverviewViewModel()
        {
            MoneyExpenses.CollectionChanged += MoneyExpensesOnCollectionChanged;

            RefreshCommand = new Command(RefreshCommandHandler);
        }

        private void MoneyExpensesOnCollectionChanged(
            object sender, 
            NotifyCollectionChangedEventArgs args)
        {
            CalculateExpensesSum();
        }

        private void CalculateExpensesSum()
        {
            double sum = MoneyExpenses.Sum(x => x.ValueExpense);
            string currencySum = sum.ToString("C2", new CultureInfo("nl-NL"));
            SumMoneyExpenses = currencySum;
        }

        private async void RefreshCommandHandler()
        {
            await Task.Delay(1000);

            var firstExpense = MoneyExpenses.FirstOrDefault();
            if (firstExpense != null)
            {
                MoneyExpenses.Remove(firstExpense);
            }

            MessagingCenter.Send<object>(this, "RefreshingComplete");
        }
    }
}