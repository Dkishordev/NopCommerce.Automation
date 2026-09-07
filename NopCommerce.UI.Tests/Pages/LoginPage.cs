using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NopCommerce.UI.Tests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
        }

        private ILocator Email =>
            _page.Locator("#Email");

        private ILocator Password =>
            _page.Locator("#Password");

        private ILocator LoginButton =>
            _page.Locator("button.login-button");

        public async Task NavigateAsync(string baseUrl)
        {
            await _page.GotoAsync(
                $"{baseUrl.TrimEnd('/')}/login");
        }

        public async Task LoginAsync(
            string email,
            string password)
        {
            await Email.FillAsync(email);

            await Password.FillAsync(password);

            await LoginButton.ClickAsync();
        }
    }
}
