using Microsoft.Playwright;
using NopCommerce.TestFramework.Configuration;
using NUnit.Framework;

namespace NopCommerce.TestFramework.Authentication;

public sealed class AuthenticationStateManager
{
    public string StateFilePath =>
        Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "auth",
            "customer.json");

    public async Task EnsureAuthenticatedStateAsync()
    {
        if (File.Exists(StateFilePath))
        {
            return;
        }

        await CreateAuthenticatedStateAsync();
    }

    public async Task CreateAuthenticatedStateAsync()
    {
        var settings = ConfigurationManager.Settings;

        if (string.IsNullOrWhiteSpace(settings.TestUser.Email) ||
            string.IsNullOrWhiteSpace(settings.TestUser.Password))
        {
            throw new InvalidOperationException(
                "TestUser.Email and TestUser.Password must be configured.");
        }

        var directory =
            Path.GetDirectoryName(StateFilePath)!;

        Directory.CreateDirectory(directory);

        IPlaywright? playwright = null;
        IBrowser? browser = null;
        IBrowserContext? context = null;

        try
        {
            playwright = await Playwright.CreateAsync();

            browser =
                await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Headless = settings.Browser.Headless
                    });

            context =
                await browser.NewContextAsync(
                    new BrowserNewContextOptions
                    {
                        IgnoreHTTPSErrors =
                            settings.Browser.IgnoreHTTPSErrors
                    });

            var page =
                await context.NewPageAsync();

            page.SetDefaultTimeout(
                settings.Browser.DefaultTimeout);

            page.SetDefaultNavigationTimeout(
                settings.Browser.NavigationTimeout);

            var loginUrl =
                $"{settings.Application.BaseUrl.TrimEnd('/')}/login";

            TestContext.Progress.WriteLine(
                $"Creating authentication state using {loginUrl}");

            await page.GotoAsync(loginUrl);

            await page.Locator("#Email")
                .FillAsync(settings.TestUser.Email);

            await page.Locator("#Password")
                .FillAsync(settings.TestUser.Password);

            await page.Locator("button.login-button")
                .ClickAsync();

            await page.WaitForLoadStateAsync(
                LoadState.NetworkIdle);

            // Important: verify login actually succeeded.
            if (page.Url.Contains("/login",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Login did not succeed. " +
                    "The browser is still on the login page.");
            }

            await context.StorageStateAsync(
                new BrowserContextStorageStateOptions
                {
                    Path = StateFilePath
                });

            TestContext.Progress.WriteLine(
                $"Authentication state created: {StateFilePath}");
        }
        finally
        {
            if (context != null)
                await context.CloseAsync();

            if (browser != null)
                await browser.CloseAsync();

            playwright?.Dispose();
        }
    }
}