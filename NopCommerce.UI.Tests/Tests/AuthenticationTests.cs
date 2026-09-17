using FluentAssertions;
using NopCommerce.TestFramework.Browser;
using NopCommerce.TestFramework.Configuration;
using NopCommerce.UI.Tests.Pages;
using NUnit.Framework;

namespace NopCommerce.UI.Tests.Tests;

[TestFixture]
[Category("UI")]
[Category("Authentication")]
public sealed class AuthenticationTests : BaseTest
{
    [Test]
    [Category("Smoke")]
    public async Task CustomerCanLogin()
    {
        var settings = ConfigurationManager.Settings;

        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();

        await loginPage.LoginAsync(
            settings.TestUser.Email,
            settings.TestUser.Password);

        Page.Url.Should().NotContain("/login");
    }

    [Test]
    public async Task CustomerCannotLoginWithInvalidPassword()
    {
        var settings = ConfigurationManager.Settings;

        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();

        await loginPage.LoginAsync(
            settings.TestUser.Email,
            "InvalidPassword123!");

        Page.Url.Should().Contain("/login");

        var error =
            await loginPage.GetLoginErrorAsync();

        error.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public async Task CustomerCannotLoginWithInvalidEmail()
    {
        var settings = ConfigurationManager.Settings;

        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();

        await loginPage.LoginAsync(
            "does-not-exist@example.com",
            settings.TestUser.Password);

        Page.Url.Should().Contain("/login");
    }

    [Test]
    public async Task LoginPageDisplaysRequiredFields()
    {
        var loginPage = new LoginPage(Page);

        await loginPage.OpenAsync();

        (await loginPage.IsEmailDisplayedAsync())
            .Should().BeTrue();

        (await loginPage.IsPasswordDisplayedAsync())
            .Should().BeTrue();
    }
}