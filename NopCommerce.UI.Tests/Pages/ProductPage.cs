using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public sealed class ProductPage : BasePage
    {
        private ILocator AddToCartButton =>
            Page.Locator(".add-to-cart-button").First;

        private ILocator ProductTitle =>
            Page.Locator(".product-name");

        public ProductPage(IPage page)
            : base(page)
        {
        }

        public async Task<string> GetProductNameAsync()
        {
            return await ProductTitle.InnerTextAsync();
        }

        public async Task AddToCartAsync()
        {
            await AddToCartButton.ClickAsync();
        }
    }
}
