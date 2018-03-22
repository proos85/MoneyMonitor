using MoneyMonitor.IoC;
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

            IoCContainer.RegisterContainer();
            DependencyService.Register<OverviewViewModel>();

		    var moneyOverviewPage = IoCContainer.GetInstance<MoneyOverviewPage>();
            MainPage = new NavigationPage(moneyOverviewPage);
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
