using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPage page)
        {
            _page = page;
        }

        private ILocator AddToCartButton =>
            _page.Locator(".add-to-cart-button").First;

        public async Task AddToCartAsync()
        {
            await AddToCartButton.ClickAsync();
        }
    }
}
