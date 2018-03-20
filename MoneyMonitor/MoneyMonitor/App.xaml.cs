using MoneyMonitor.Pages;
using MoneyMonitor.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class App
    {
		public App ()
		{
			InitializeComponent();

		    DependencyService.Register<OverviewViewModel>();

            MainPage = new NavigationPage(new MoneyOverviewPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
