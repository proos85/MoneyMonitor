using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MoneyMonitor.Droid
{
    [Activity(Label = "MoneyMonitor", Icon = "@drawable/icon", NoHistory = true, Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SpashActivity: Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Finish();
            OverridePendingTransition(0, 0);
        }
    }
}