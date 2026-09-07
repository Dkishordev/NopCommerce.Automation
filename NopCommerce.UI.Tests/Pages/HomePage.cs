using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public class HomePage
    {
        private readonly IPage _page;

        public HomePage(IPage page)
        {
            _page = page;
        }

        private ILocator SearchBox =>
            _page.Locator("#small-searchterms");

        private ILocator SearchButton =>
            _page.Locator("button.search-box-button");

        public async Task SearchAsync(string searchTerm)
        {
            await SearchBox.FillAsync(searchTerm);

            await SearchButton.ClickAsync();
        }
    }
}
