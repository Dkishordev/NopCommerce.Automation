using FluentAssertions;
using NopCommerce.TestFramework.Browser;
using NopCommerce.TestFramework.Configuration;
using NopCommerce.UI.Tests.Pages;


namespace NopCommerce.UI.Tests.Tests;

[TestFixture]
public sealed class CheckoutTests : AuthenticatedTestBase
{
    [Test]
    public async Task CustomerCanLoginAndAddProductToCart()
    {
        var settings =
            ConfigurationManager.Settings;

        var loginPage =
            new LoginPage(Page);

        var homePage =
            new HomePage(Page);

        var searchPage =
            new SearchPage(Page);

        var productPage =
            new ProductPage(Page);

        var cartPage =
            new CartPage(Page);

        // 1. Login
        await Page.GotoAsync(
              settings.Application.BaseUrl);
        // 2. Search product
        await homePage.SearchAsync("Apple MacBook Pro");

        // 3. Open first result
        await searchPage.OpenFirstProductAsync();

        // 4. Add product to cart
        await productPage.AddToCartAsync();

        // 5. Go to cart
        await Page.GotoAsync(
            $"{settings.Application.BaseUrl.TrimEnd('/')}/cart");

        // 6. Checkout
        await cartPage.CheckoutAsync();

        // Basic assertion for now
        Page.Url.Should().Contain("checkout");
    }
}