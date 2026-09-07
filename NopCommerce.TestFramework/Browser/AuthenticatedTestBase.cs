using Microsoft.Playwright;
using NopCommerce.TestFramework.Configuration;
using NUnit.Framework;

namespace NopCommerce.TestFramework.Browser;

public abstract class AuthenticatedTestBase : BaseTest
{
    protected override async Task ConfigureContextAsync(
        BrowserNewContextOptions options)
    {
        var authStatePath =
            Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "auth",
                "customer-state.json");

        if (File.Exists(authStatePath))
        {
            options.StorageStatePath = authStatePath;
        }

        await base.ConfigureContextAsync(options);
    }
}
