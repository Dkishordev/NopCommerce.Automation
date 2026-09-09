using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopCommerce.UI.Tests.Pages
{
    public sealed class HomePage: BasePage
    {
        private ILocator SearchBox =>
            Page.Locator("#small-searchterms");

        private ILocator SearchButton =>
            Page.Locator("button.search-box-button");

        public HomePage(IPage page): base(page) 
        {
        }

 

        public async Task SearchAsync(string searchTerm)
        {
            await SearchBox.FillAsync(searchTerm);

            await SearchButton.ClickAsync();

            await Page.WaitForLoadStateAsync(
                LoadState.NetworkIdle);
        }
    }
}
