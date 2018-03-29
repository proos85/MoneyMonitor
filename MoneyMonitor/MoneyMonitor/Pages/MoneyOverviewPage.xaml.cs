using System.Threading.Tasks;
using Autofac;
using MoneyMonitor.Client.Overview;
using MoneyMonitor.IoC;
using MoneyMonitor.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoneyOverviewPage
	{
	    private readonly IOverviewClient _overviewClient;

	    public MoneyOverviewPage()
	    {
	        InitializeComponent();
        }

	    public MoneyOverviewPage(IOverviewClient overviewClient)
		{
		    _overviewClient = overviewClient;

		    InitializeComponent();

		    MessagingCenter.Subscribe<object>(this, "RefreshingComplete", sender => ListviewRefreshingComplete());

		    BindingContext = DependencyContainer.GetInstance<OverviewViewModel>();
		}

	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

	        await LoadExpensesAsync();
	    }

	    private async Task LoadExpensesAsync()
	    {
	        var vm = (OverviewViewModel) BindingContext;
            foreach (var expense in await _overviewClient.LoadMoneyExpensesAsync())
	        {
	            vm.MoneyExpenses.Add(expense);
	        }
	    }

	    private void ListviewRefreshingComplete()
	    {
	        OverviewList.IsRefreshing = false;
	    }
	}
}