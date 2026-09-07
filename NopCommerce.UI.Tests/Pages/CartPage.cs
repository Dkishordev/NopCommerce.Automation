using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public class CartPage
    {
        private readonly IPage _page;

        public CartPage(IPage page)
        {
            _page = page;
        }

        private ILocator CheckoutButton =>
            _page.Locator(".checkout-button");

        private ILocator TermOfService =>
            _page.Locator("#termsofservice");

        public async Task CheckoutAsync()
        {
            await TermOfService.ClickAsync();
            await CheckoutButton.ClickAsync();
        }
    }
}
