using System.Globalization;
using MoneyMonitor.IoC;
using MoneyMonitor.Pages;
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

            SetToDutchCulture();

            RegisterDependencies();
            
		    SetMainPage();
		}

        private static void SetToDutchCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
        }

        private static void RegisterDependencies()
        {
            DependencyContainer.RegisterContainer();
        }

        private void SetMainPage()
        {
            var navPage = new NavigationPage(DependencyContainer.GetInstance<MoneyOverviewPage>())
            {
                BarBackgroundColor = (Color)Current.Resources["StatusBarBackgroundColor"],
                BarTextColor = (Color)Current.Resources["StatusBarTextColor"]
            };

            MainPage = navPage;
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
