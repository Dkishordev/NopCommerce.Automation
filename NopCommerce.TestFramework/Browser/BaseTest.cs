using Microsoft.Playwright;
using NUnit.Framework;

namespace NopCommerce.TestFramework.Browser;

public abstract class BaseTest
{
    protected IPlaywright Playwright { get; private set; } = null!;

    protected IBrowser Browser { get; private set; } = null!;

    protected IBrowserContext Context { get; private set; } = null!;

    protected IPage Page { get; private set; } = null!;

    private BrowserFactory _browserFactory = null!;

    [SetUp]
    public async Task BaseSetUp()
    {
        _browserFactory = new BrowserFactory();

        Playwright =
            await _browserFactory.CreatePlaywrightAsync();

        Browser =
            await _browserFactory.CreateBrowserAsync(
                Playwright);

        var contextOptions =
            _browserFactory.CreateContextOptions();

        await ConfigureContextAsync(contextOptions);

        Context =
            await Browser.NewContextAsync(
                contextOptions);

        Page =
            await Context.NewPageAsync();

        await Context.Tracing.StartAsync(
            new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

        await OnTestStartAsync();
    }

    protected virtual Task ConfigureContextAsync(
        BrowserNewContextOptions options)
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnTestStartAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnTestEndAsync()
    {
        return Task.CompletedTask;
    }

    [TearDown]
    public async Task BaseTearDown()
    {
        await OnTestEndAsync();

        var testStatus =
            TestContext.CurrentContext.Result.Outcome.Status;

        var testName =
            TestContext.CurrentContext.Test.Name;

        var artifactsDirectory =
            Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "TestResults",
                "Artifacts");

        Directory.CreateDirectory(artifactsDirectory);

        if (testStatus ==
            NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var safeTestName =
                string.Join(
                    "_",
                    testName.Split(
                        Path.GetInvalidFileNameChars()));

            var screenshotPath =
                Path.Combine(
                    artifactsDirectory,
                    $"{safeTestName}.png");

            var tracePath =
                Path.Combine(
                    artifactsDirectory,
                    $"{safeTestName}.zip");

            await Page.ScreenshotAsync(
                new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });

            await Context.Tracing.StopAsync(
                new TracingStopOptions
                {
                    Path = tracePath
                });

            TestContext.AddTestAttachment(
                screenshotPath);

            TestContext.AddTestAttachment(
                tracePath);
        }
        else
        {
            await Context.Tracing.StopAsync();
        }

        await Context.CloseAsync();
        await Browser.CloseAsync();

        Playwright.Dispose();
    }
}