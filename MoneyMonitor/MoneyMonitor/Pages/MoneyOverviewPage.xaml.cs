using Autofac;
using MoneyMonitor.Client.Overview;
using MoneyMonitor.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoneyOverviewPage
	{
	    private readonly IOverviewClient _overviewClient;

	    public MoneyOverviewPage(IOverviewClient overviewClient)
		{
		    _overviewClient = overviewClient;

		    InitializeComponent ();

		    MessagingCenter.Subscribe<object>(this, "RefreshingComplete", sender => ListviewRefreshingComplete());

		    BindingContext = DependencyService.Get<OverviewViewModel>();
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        var expenses = _overviewClient.LoadMoneyExpenses();

            var viewModel = DependencyService.Get<OverviewViewModel>();
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 1", TypeExpense = "Vast", ValueExpense = 100});
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 2", TypeExpense = "Variabel" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 3", TypeExpense = "Gezamelijk" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 4", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 5", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 6", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 7", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 8", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 9", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 10", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 11", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 12", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 13", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 14", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 15", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 16", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 17", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 18", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 19", TypeExpense = "Vast" });
            viewModel.MoneyExpenses.Add(new MoneyExpenseViewModel { NameExpense = "Uitgave 20", TypeExpense = "Vast" });
        }

	    private void ListviewRefreshingComplete()
	    {
	        OverviewList.IsRefreshing = false;
	    }
	}
}