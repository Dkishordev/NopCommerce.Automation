using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public class SearchPage
    {
        private readonly IPage _page;

        public SearchPage(IPage page)
        {
            _page = page;
        }

        private ILocator FirstProduct =>
            _page.Locator(".product-title a").First;

        public async Task OpenFirstProductAsync()
        {
            await FirstProduct.ClickAsync();
        }
    }
}
