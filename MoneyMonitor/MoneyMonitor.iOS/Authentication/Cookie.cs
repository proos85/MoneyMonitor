using Foundation;
using MoneyMonitor.Authentication;
using MoneyMonitor.iOS.Authentication;
using Xamarin.Forms;

[assembly: Dependency(typeof(Cookie))]
namespace MoneyMonitor.iOS.Authentication
{
    public class Cookie: ICookie
    {
        public Cookie()
        {
        }

        public void RemoveCookies()
        {
            var cookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in cookieStorage.Cookies)
            {
                cookieStorage.DeleteCookie(cookie);
            }

            NSUrlCache.SharedCache.RemoveAllCachedResponses();
            NSUrlCache.SharedCache.DiskCapacity = 0;
            NSUrlCache.SharedCache.MemoryCapacity = 0;

            NSUserDefaults.StandardUserDefaults.Synchronize();
        }
    }
}