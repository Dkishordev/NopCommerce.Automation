using NopCommerce.TestFramework.Browser;
using NopCommerce.UI.Tests.Pages;
using NopCommerce.TestFramework.Configuration;
using FluentAssertions;


namespace NopCommerce.UI.Tests.Tests
{

    [TestFixture]
    [Category("UI")]
    [Category("Product")]
    public sealed class ProductDetailsTests : AuthenticatedTestBase
    {
        [Test]
        [Category("Smoke")]
        public async Task ProductDisplaysRequiredInformation()
        {
            var productPage = new ProductPage(Page);
            var searchPage = new SearchPage(Page);
            var settings = ConfigurationManager.Settings;
            await Page.GotoAsync(
                settings.Application.BaseUrl);

            await Page.GotoAsync(
                $"{settings.Application.BaseUrl.TrimEnd('/')}/computers");

            await productPage.ClickFirstProductAsync();
            await productPage.OpenFirstProductAsync();


            var productName =
                await productPage.GetProductNameAsync();

            var productPrice =
                await productPage.GetProductPriceAsync();

            productName.Should().NotBeNullOrWhiteSpace();
            productPrice.Should().NotBeNullOrWhiteSpace();
        }
    }

}
