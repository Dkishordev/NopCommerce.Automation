using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace NopCommerce.UI.Tests.Pages
{
    public sealed class RegisterPage : BasePage
    {
        public RegisterPage(IPage page) : base(page)
        {
        }

        private ILocator Firstname =>
            Page.Locator("#FirstName");
        private ILocator Lastname =>
            Page.Locator("#LastName");

        private ILocator Email =>
            Page.Locator("#Email");

        private ILocator Company =>
            Page.Locator("#Company");

        private ILocator Password =>
            Page.Locator("#Password");

        private ILocator ConfirmPassword =>
            Page.Locator("#ConfirmPassword");



        private ILocator RegisterButton =>
            Page.Locator("button.register-next-step-button");

        private ILocator TextMessage =>
           Page.Locator(".result");

        public async Task OpenAsync()
        {
            await NavigateAsync("register");
        }

        public async Task RegisterAsync(
            string firstname, string lastname, string email, string company,
             string password, string confirmpassword)
        {
            await Firstname.FillAsync(firstname);
            await Lastname.FillAsync(lastname);
            await Email.FillAsync(email);
            await Company.FillAsync(company);
            await Password.FillAsync(password);
            await ConfirmPassword.FillAsync(confirmpassword);

            await RegisterButton.ClickAsync();

            await Page.WaitForLoadStateAsync(
                LoadState.NetworkIdle);
        }

        public async Task<string> RegisterMessageAsync()
        {
            return await TextMessage.InnerTextAsync();
        }



    }
}
