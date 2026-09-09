using Microsoft.Playwright;
using NopCommerce.TestFramework.Authentication;

namespace NopCommerce.TestFramework.Browser;

public abstract class AuthenticatedTestBase : BaseTest
{
    protected override async Task ConfigureContextAsync(
        BrowserNewContextOptions options)
    {
        var authManager =
            new AuthenticationStateManager();

        await authManager.EnsureAuthenticatedStateAsync();

        options.StorageStatePath =
            authManager.StateFilePath;
    }
}