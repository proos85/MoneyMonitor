using System.Collections.ObjectModel;
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

        public ICommand RefreshCommand { get; set; }

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

        public OverviewViewModel()
        {
            RefreshCommand = new Command(RefreshCommandHandler);
        }

    }
}