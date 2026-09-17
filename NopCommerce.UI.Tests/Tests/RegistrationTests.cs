using FluentAssertions;
using NopCommerce.TestFramework.Browser;
using NopCommerce.TestFramework.Configuration;
using NopCommerce.UI.Tests.Pages;


namespace NopCommerce.UI.Tests.Tests
{
    public sealed class RegistrationTests : BaseTest
    {
        [Test]
        [Category("Smoke")]
        public async Task CustomerCanRegister()
        {
            var settings = ConfigurationManager.Settings;

            var registerPage = new RegisterPage(Page);

            

            await registerPage.OpenAsync();

            await registerPage.RegisterAsync(
                settings.TestRegisterUser.Firstname,
                settings.TestRegisterUser.Lastname,
                settings.TestRegisterUser.Email,
                settings.TestRegisterUser.Company,
                settings.TestRegisterUser.Password,
                settings.TestRegisterUser.Password);
                var message = await registerPage.RegisterMessageAsync();
                message.Should().Contain("Your registration completed");
        }

    }
}
