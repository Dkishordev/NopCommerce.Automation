using FluentAssertions;
using Microsoft.Playwright;
using NopCommerce.TestFramework.Browser;
using NopCommerce.TestFramework.Configuration;
using NUnit.Framework;

namespace NopCommerce.UI.Tests.Tests
{
    [TestFixture]
    public class HomePageTests
    {
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;
        private IPage _page = null!;

        [SetUp]
        public async Task SetUp()
        {
            _playwright = await Playwright.CreateAsync();

            var browserFactory = new BrowserFactory();

            _browser = await browserFactory.CreateBrowserAsync(_playwright);

            _page = await _browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public async Task NopCommerce_HomePage_Should_Load()
        {
            var baseUrl =
                ConfigurationManager.Settings.Application.BaseUrl;

            await _page.GotoAsync(baseUrl);

            var title = await _page.TitleAsync();

            title.Should().NotBeNullOrWhiteSpace();
        }
    }
}
