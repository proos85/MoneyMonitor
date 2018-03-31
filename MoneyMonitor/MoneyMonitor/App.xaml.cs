using System.Globalization;
using Microsoft.Identity.Client;
using MoneyMonitor.IoC;
using MoneyMonitor.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class App
    {
        public static UIParent UiParent = null;

        public App ()
		{
			InitializeComponent();

            SetToDutchCulture();

            RegisterDependencies();

		    SetLoginPage();
		}

        public static void SetLoginPage()
        {
            Current.MainPage = DependencyContainer.GetInstance<LoginPage>();
        }

        public static void SetMainPage()
        {
            var navPage = new NavigationPage(DependencyContainer.GetInstance<MoneyOverviewPage>())
            {
                BarBackgroundColor = (Color)Current.Resources["StatusBarBackgroundColor"],
                BarTextColor = (Color)Current.Resources["StatusBarTextColor"]
            };

            Current.MainPage = navPage;
        }

        private static void SetToDutchCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
        }

        private static void RegisterDependencies()
        {
            DependencyContainer.RegisterContainer();
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
