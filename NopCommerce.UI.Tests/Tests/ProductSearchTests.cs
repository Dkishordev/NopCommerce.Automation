using FluentAssertions;
using NopCommerce.TestFramework.Browser;
using NopCommerce.UI.Tests.Pages;
using NopCommerce.TestFramework.Configuration;
using static Dapper.SqlMapper;

namespace NopCommerce.UI.Tests.Tests
{
    [TestFixture]
    [Category("UI")]
    [Category("Product")]
    public sealed class ProductSearchTests : AuthenticatedTestBase
    {
        [Test]
        [Category("Smoke")]
        public async Task CustomerCanSearchForProduct()
        {
            var homePage = new HomePage(Page);
            var searchPage = new SearchPage(Page);
            var settings = ConfigurationManager.Settings;
            await Page.GotoAsync(
                settings.Application.BaseUrl);

            await homePage.SearchAsync("computer");

            var count =
                await searchPage.GetProductCountAsync();

            count.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task SearchForUnknownProductReturnsNoResults()
        {
            var homePage = new HomePage(Page);
            var searchPage = new SearchPage(Page);
            var settings = ConfigurationManager.Settings;

            await Page.GotoAsync(
                settings.Application.BaseUrl);

            await homePage.SearchAsync(
                "ProductThatDefinitelyDoesNotExist123456");

            var count =
                await searchPage.GetProductCountAsync();

            count.Should().Be(0);
        }
    }

}
