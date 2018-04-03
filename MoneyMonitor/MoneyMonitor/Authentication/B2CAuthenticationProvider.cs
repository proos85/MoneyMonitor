using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;

namespace MoneyMonitor.Authentication
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class B2CAuthenticationProvider : IB2CAuthenticationProvider
    {
        private readonly ICookie _cookies;
        private PublicClientApplication _b2CApplication;

        // Azure AD B2C Coordinates
        const string Tenant = "proos85.onmicrosoft.com";
        const string ClientId = "1f2ab3e0-9e64-4043-94b2-35bd249fd7ea";
        const string PolicySignIn = "B2C_1_SignIn";

        readonly string[] _scopes = { "https://proos85.onmicrosoft.com/backend/monitor.read" };
        readonly string _authorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
        readonly string _authority;

        public AuthenticationResult AuthenticationResult { get; private set; }

        public B2CAuthenticationProvider(ICookie cookies)
        {
            _cookies = cookies;
            _authority = $"{_authorityBase}{PolicySignIn}";

            Initialize();
        }

        public async Task<bool> TryAuthorizeWithB2CAsync()
        {
            try
            {
                AuthenticationResult = await _b2CApplication.AcquireTokenSilentAsync(
                    _scopes,
                    GetUserByPolicy(_b2CApplication.Users, PolicySignIn),
                    _authority,
                    false).ConfigureAwait(false);

                return true;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        public async Task<bool> AuthorizeWithB2CAsync()
        {
            try
            {
                AuthenticationResult = await _b2CApplication.AcquireTokenAsync(
                    _scopes,
                    GetUserByPolicy(_b2CApplication.Users, PolicySignIn),
                    App.UiParent).ConfigureAwait(false);

                return true;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return false;
            }
        }

        public void LogoutB2C()
        {
            foreach (var user in _b2CApplication.Users)
            {
                _b2CApplication.Remove(user);
            }

            _cookies.RemoveCookies();
        }

        void Initialize()
        {
            _b2CApplication = new PublicClientApplication(ClientId, _authority)
            {
                ValidateAuthority = false,
                RedirectUri = $"msal{ClientId}://auth"
            };
        }

        IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower())) return user;
            }

            return null;
        }

        string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return decoded;
        }

        JObject ParseIdToken(string idToken)
        {
            // Get the piece with actual user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }
    }
}