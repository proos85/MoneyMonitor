using System;
using System.Threading.Tasks;
using MoneyMonitor.Authentication;
using MoneyMonitor.IoC;
using MoneyMonitor.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MoneyOverviewPage
	{
	    //private readonly IOverviewClient _overviewClient;
	    private readonly IB2CAuthenticationProvider _authenticationProvider;

	    public MoneyOverviewPage()
	    {
	        InitializeComponent();
        }

	    public MoneyOverviewPage(
	        /*IOverviewClient overviewClient,*/
	        IB2CAuthenticationProvider authenticationProvider)
		{
		    //_overviewClient = overviewClient;
		    _authenticationProvider = authenticationProvider;

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
            await Task.Run(() =>
            {
                var vm = (OverviewViewModel)BindingContext;
                vm.RetrieveExpensesCommand.Execute(null);
            });
	    }

	    private void ListviewRefreshingComplete()
	    {
	        OverviewList.IsRefreshing = false;
	    }

	    private void MenuItem_OnClicked(object sender, EventArgs e)
	    {
	        _authenticationProvider.LogoutB2C();
            App.SetLoginPage();
	    }
	}
}