using System.Windows.Input;
using MoneyMonitor.Authentication;
using MoneyMonitor.ViewModel.Base;
using Xamarin.Forms;

namespace MoneyMonitor.ViewModel
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SplashViewModel: BaseViewModel
    {
        private readonly IB2CAuthenticationProvider _authenticationProvider;

        public ICommand AuthenticateCommand => new Command(async () =>
        {
            var isAuthenticated = await _authenticationProvider.TryAuthorizeWithB2CAsync().ConfigureAwait(false);
            MessagingCenter.Send<object,bool>(this, "IsUserAuthenticated", isAuthenticated);
        });

        public SplashViewModel(IB2CAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }
    }
}