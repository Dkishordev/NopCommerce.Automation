using Microsoft.Playwright;
using NopCommerce.TestFramework.Configuration;

namespace NopCommerce.TestFramework.Authentication;

public class AuthenticationStateManager
{
    public async Task CreateCustomerStateAsync(
        IPage page,
        string stateFilePath)
    {
        var settings =
            ConfigurationManager.Settings;

        await page.GotoAsync(
            $"{settings.Application.BaseUrl.TrimEnd('/')}/login");

        await page.Locator("#Email")
            .FillAsync(settings.TestUser.Email);

        await page.Locator("#Password")
            .FillAsync(settings.TestUser.Password);

        await page.Locator("button.login-button")
            .ClickAsync();

        await page.Context.StorageStateAsync(
            new BrowserContextStorageStateOptions
            {
                Path = stateFilePath
            });
    }
}
