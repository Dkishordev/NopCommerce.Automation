using Microsoft.Playwright;
using NopCommerce.TestFramework.Configuration;

namespace NopCommerce.TestFramework.Browser;

public sealed class BrowserFactory
{
    public async Task<IPlaywright> CreatePlaywrightAsync()
    {
        return await Playwright.CreateAsync();
    }

    public async Task<IBrowser> CreateBrowserAsync(
        IPlaywright playwright)
    {
        var settings = ConfigurationManager.Settings.Browser;

        var options = new BrowserTypeLaunchOptions
        {
            Headless = settings.Headless
        };

        return settings.Browser.ToLowerInvariant() switch
        {
            "chromium" =>
                await playwright.Chromium.LaunchAsync(options),

            "firefox" =>
                await playwright.Firefox.LaunchAsync(options),

            "webkit" =>
                await playwright.Webkit.LaunchAsync(options),

            _ => throw new ArgumentException(
                $"Unsupported browser: {settings.Browser}")
        };
    }

    public BrowserNewContextOptions CreateContextOptions()
    {
        var settings = ConfigurationManager.Settings.Browser;

        return new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = settings.IgnoreHTTPSErrors,

            ViewportSize = new ViewportSize
            {
                Width = 1440,
                Height = 900
            },

            Locale = "en-GB",

            ColorScheme = ColorScheme.Light
        };
    }
}