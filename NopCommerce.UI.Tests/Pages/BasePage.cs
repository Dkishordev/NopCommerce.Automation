using Microsoft.Playwright;
using NopCommerce.TestFramework.Configuration;

namespace NopCommerce.UI.Tests.Pages;

public abstract class BasePage
{
    protected IPage Page { get; }

    protected string BaseUrl =>
        ConfigurationManager.Settings
            .Application
            .BaseUrl
            .TrimEnd('/');

    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected async Task NavigateAsync(string path)
    {
        await Page.GotoAsync(
            $"{BaseUrl}/{path.TrimStart('/')}");
    }
}