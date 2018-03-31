using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace MoneyMonitor.Authentication
{
    public interface IB2CAuthenticationProvider
    {
        Task<bool> TryAuthorizeWithB2CAsync();

        Task<bool> AuthorizeWithB2CAsync();

        void LogoutB2C();

        AuthenticationResult AuthenticationResult { get; }
    }
}