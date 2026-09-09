using Microsoft.Playwright;
using NUnit.Framework;
using NopCommerce.TestFramework.Configuration;

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
            await Browser.NewContextAsync(contextOptions);

        Page =
            await Context.NewPageAsync();

        var browserSettings =
            ConfigurationManager.Settings.Browser;

        Page.SetDefaultTimeout(
            browserSettings.DefaultTimeout);

        Page.SetDefaultNavigationTimeout(
            browserSettings.NavigationTimeout);

        await Context.Tracing.StartAsync(
            new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

        TestContext.Progress.WriteLine(
            $"Starting test: {TestContext.CurrentContext.Test.Name}");

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
        try
        {
            await OnTestEndAsync();

            var failed =
                TestContext.CurrentContext.Result.Outcome.Status
                == NUnit.Framework.Interfaces.TestStatus.Failed;

            if (failed)
            {
                await CaptureFailureArtifactsAsync();
            }
            else
            {
                await StopTracingAsync();
            }
        }
        catch (Exception ex)
        {
            TestContext.Progress.WriteLine(
                $"Teardown error: {ex}");
        }
        finally
        {
            await CleanupAsync();
        }
    }

    private async Task CaptureFailureArtifactsAsync()
    {
        var artifactDirectory =
            Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "TestResults",
                "Artifacts");

        Directory.CreateDirectory(artifactDirectory);

        var testName =
            SanitizeFileName(
                TestContext.CurrentContext.Test.Name);

        var uniqueId =
            Guid.NewGuid().ToString("N")[..8];

        var screenshotPath =
            Path.Combine(
                artifactDirectory,
                $"{testName}_{uniqueId}.png");

        var tracePath =
            Path.Combine(
                artifactDirectory,
                $"{testName}_{uniqueId}.zip");

        if (Page != null)
        {
            await Page.ScreenshotAsync(
                new PageScreenshotOptions
                {
                    Path = screenshotPath,
                    FullPage = true
                });
        }

        if (Context != null)
        {
            await Context.Tracing.StopAsync(
                new TracingStopOptions
                {
                    Path = tracePath
                });
        }

        TestContext.AddTestAttachment(screenshotPath);
        TestContext.AddTestAttachment(tracePath);

        TestContext.Progress.WriteLine(
            $"Screenshot: {screenshotPath}");

        TestContext.Progress.WriteLine(
            $"Trace: {tracePath}");
    }

    private async Task StopTracingAsync()
    {
        if (Context != null)
        {
            await Context.Tracing.StopAsync();
        }
    }

    private async Task CleanupAsync()
    {
        try
        {
            if (Context != null)
                await Context.CloseAsync();
        }
        catch
        {
            // Ignore cleanup failures.
        }

        try
        {
            if (Browser != null)
                await Browser.CloseAsync();
        }
        catch
        {
            // Ignore cleanup failures.
        }

        try
        {
            Playwright?.Dispose();
        }
        catch
        {
            // Ignore cleanup failures.
        }
    }

    private static string SanitizeFileName(string value)
    {
        foreach (var character in Path.GetInvalidFileNameChars())
        {
            value = value.Replace(character, '_');
        }

        return value.Length > 150
            ? value[..150]
            : value;
    }
}