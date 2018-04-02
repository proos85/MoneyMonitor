using System.Threading.Tasks;
using MoneyMonitor.Authentication;
using Xamarin.Forms.Xaml;

namespace MoneyMonitor.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
	    private readonly IB2CAuthenticationProvider _authenticationProvider;

	    public LoginPage (IB2CAuthenticationProvider authenticationProvider)
	    {
	        _authenticationProvider = authenticationProvider;

	        InitializeComponent ();
	    }

	    protected override async void OnAppearing()
	    {
	        bool userIsAutenticated = await AuthenticateUser();
	        if (userIsAutenticated)
	        {
                App.SetMainPage();
	        }
	    }

	    private async Task<bool> AuthenticateUser()
	    {
	        var hasAccess = await _authenticationProvider.TryAuthorizeWithB2CAsync();
	        if (!hasAccess)
	        {
	            hasAccess = await _authenticationProvider.AuthorizeWithB2CAsync();
	        }

	        return hasAccess;
	    }
	}
}