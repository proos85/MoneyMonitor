using MoneyMonitor.Authentication;
using MoneyMonitor.Droid.Authentication;
using Xamarin.Forms;

[assembly: Dependency(typeof(Cookie))]
namespace MoneyMonitor.Droid.Authentication
{
    public class Cookie: ICookie
    {
        public Cookie()
        {
        }

        public void RemoveCookies()
        {
            Android.Webkit.CookieManager.Instance.RemoveAllCookies(null);
        }
    }
}