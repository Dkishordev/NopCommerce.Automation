using NopCommerce.TestFramework.Authentication;
using NopCommerce.TestFramework.Browser;
using NUnit.Framework;

namespace NopCommerce.UI.Tests.Fixtures;

[SetUpFixture]
public sealed class TestEnvironment
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        var authManager =
            new AuthenticationStateManager();

        await authManager.CreateAuthenticatedStateAsync();
    }

    [OneTimeTearDown]
    public Task Cleanup()
    {
        return Task.CompletedTask;
    }
}