using FluentAssertions;
using NopCommerce.TestFramework.Browser;
using NopCommerce.TestFramework.Configuration;
using NopCommerce.UI.Tests.Pages;
using NUnit.Framework;

namespace NopCommerce.UI.Tests.Tests;

[TestFixture]
public sealed class AuthenticationTests : BaseTest
{
    [Test]
    public async Task CustomerCanLogin()
    {
        var settings =
            ConfigurationManager.Settings;

        var loginPage =
            new LoginPage(Page);

        await loginPage.OpenAsync();

        await loginPage.LoginAsync(
            settings.TestUser.Email,
            settings.TestUser.Password);

        Page.Url.Should().NotContain("/login");
    }
}