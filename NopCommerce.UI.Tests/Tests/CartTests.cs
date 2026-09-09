using FluentAssertions;
using NopCommerce.TestFramework.Browser;
using NopCommerce.TestFramework.Configuration;
using NopCommerce.UI.Tests.Pages;

namespace NopCommerce.UI.Tests.Tests
{
    [TestFixture]
    public sealed class CartTests : AuthenticatedTestBase
    {
        [Test]
        public async Task CustomerCanAddProductToCart()
        {
            var settings =
                ConfigurationManager.Settings;

            var homePage =
                new HomePage(Page);

            var searchPage =
                new SearchPage(Page);

            var productPage =
                new ProductPage(Page);

            var cartPage =
                new CartPage(Page);

            await Page.GotoAsync(
                settings.Application.BaseUrl);

            await homePage.SearchAsync("Apple MacBook Pro");

            var productCount =
                await searchPage.GetProductCountAsync();

            productCount.Should().BeGreaterThan(0);

            await searchPage.OpenFirstProductAsync();

            var productName =
                await productPage.GetProductNameAsync();

            productName.Should().NotBeNullOrWhiteSpace();

            await productPage.AddToCartAsync();

            await Page.GotoAsync(
                $"{settings.Application.BaseUrl.TrimEnd('/')}/cart");

            var cartItemCount =
                await cartPage.GetItemCountAsync();

            cartItemCount.Should().BeGreaterThan(0);
        }
    }
}
