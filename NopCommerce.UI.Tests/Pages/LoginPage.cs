using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;

namespace NopCommerce.UI.Tests.Pages;

public sealed class LoginPage : BasePage
{
    private ILocator Email =>
        Page.Locator("#Email");

    private ILocator Password =>
        Page.Locator("#Password");

    private ILocator LoginButton =>
        Page.Locator("button.login-button");

    public LoginPage(IPage page)
        : base(page)
    {
    }

    public async Task OpenAsync()
    {
        await NavigateAsync("login");
    }

    public async Task LoginAsync(
        string email,
        string password)
    {
        await Email.FillAsync(email);
        await Password.FillAsync(password);

        await LoginButton.ClickAsync();

        await Page.WaitForLoadStateAsync(
            LoadState.NetworkIdle);
    }

    public async Task<bool> IsDisplayedAsync()
    {
        return await LoginButton.IsVisibleAsync();
    }
}