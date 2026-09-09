using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public sealed class SearchPage : BasePage
    {
        private ILocator Products =>
       Page.Locator(".product-item");

        public SearchPage(IPage page)
            : base(page)
        {
        }

        public async Task<int> GetProductCountAsync()
        {
            return await Products.CountAsync();
        }

        public async Task OpenFirstProductAsync()
        {
            await Products
                .First
                .Locator(".product-title a")
                .ClickAsync();
        }

    }
}
