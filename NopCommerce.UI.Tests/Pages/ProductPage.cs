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

        private ILocator ProductPrice =>
            Page.Locator(".product-price");

        private ILocator Products =>
        Page.Locator(".item-box");

        private ILocator ListProducts =>
        Page.Locator(".product-item");

        public ProductPage(IPage page)
            : base(page)
        {
        }

        public async Task<string> GetProductNameAsync()
        {
            return await ProductTitle.InnerTextAsync();
        }

        public async Task<string> GetProductPriceAsync() 
        { 
            return await ProductPrice.InnerTextAsync();
        }

        public async Task OpenFirstProductAsync()
        {
            await ListProducts
                .First
                .Locator(".product-title a")
                .ClickAsync();
        }

        public async Task ClickFirstProductAsync()
        {
            await Products
                .First
                .Locator(".picture a")
                .ClickAsync();
        }

        public async Task AddToCartAsync()
        {
            await AddToCartButton.ClickAsync();
        }
    }
}
