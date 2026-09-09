using NopCommerce.TestFramework.Authentication;
using NUnit.Framework;

namespace NopCommerce.TestFramework.Browser;

[SetUpFixture]
public class AuthenticationSetup
{
    [OneTimeSetUp]
    public async Task CreateAuthenticationState()
    {
        var authManager = new AuthenticationStateManager();

        await authManager.CreateAuthenticatedStateAsync();
    }
}