using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public sealed class CartPage : BasePage
    {

        public CartPage(IPage page):base(page)
        {
        }

        private ILocator CartItems =>
            Page.Locator("table.cart tbody tr");

        private ILocator CheckoutButton =>
            Page.Locator(".checkout-button");

        private ILocator TermOfService =>
            Page.Locator("#termsofservice");

        public async Task<int> GetItemCountAsync()
        {
            return await CartItems.CountAsync();
        }

        public async Task CheckoutAsync()
        {
            await TermOfService.ClickAsync();
            await CheckoutButton.ClickAsync();
            await Page.WaitForLoadStateAsync(
                LoadState.NetworkIdle);
        }
    }
}
