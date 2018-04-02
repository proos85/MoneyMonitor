using System;
using System.Threading.Tasks;
using MoneyMonitor.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage
	{
	    public SplashPage (MainViewModel vm)
	    {
	        InitializeComponent();

	        BindingContext = vm.SplashViewModel;
	    }

	    protected override async void OnAppearing()
	    {
	        await ShowLoadingEffect();

	        TryToSeeIfUserIsAuthenticated();
	    }

	    void TryToSeeIfUserIsAuthenticated()
	    {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
	        {
	            MessagingCenter.Subscribe<object, bool>(this, "IsUserAuthenticated", (sender, isAuthenticated) =>
	            {
	                MessagingCenter.Unsubscribe<object, bool>(this, "IsUserAuthenticated");

	                if (!isAuthenticated)
	                {
                        App.SetLoginPage();
	                }
	                else
	                {
	                    App.SetMainPage();
	                }
                });


                var vm = (SplashViewModel) BindingContext;
                vm.AuthenticateCommand.Execute(null);
                return false;
	        });
	    }

	    async Task ShowLoadingEffect()
	    {
	        await Task.Delay(2000).ConfigureAwait(false);

	        await Task.WhenAll(
	            SplashGrid.FadeTo(0, 2000),
	            Logo.ScaleTo(10, 2000)
	        );
	    }
	}
}