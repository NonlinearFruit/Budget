using System.IO;
using System.Threading.Tasks;
using Budget.Tests.Utilities;
using Microsoft.Playwright;
using Xunit;

namespace Budget.Tests;

public class EndToEndTests : IClassFixture<PlaywrightFixture>
{
    private readonly IPlaywright _playwright;
    private readonly string _imageFolder;

    public EndToEndTests(PlaywrightFixture fixture)
    {
        _playwright = fixture.Playwright;
        _imageFolder = fixture.ScreenshotFolder;
    }

    [ContinuousIntegrationOnlyFact]
    public async Task screenshot_the_home_page()
    {
        var browser = await CreateBrowser();
        var page = await GetBudgetPage(browser, Blazor.Constants.HomePath);
        await ClickGreenButton(page);
        await TakeScreenshot(page, "home");
    }

    [ContinuousIntegrationOnlyFact]
    public async Task screenshot_the_transactions_page()
    {
        var browser = await CreateBrowser();
        var page = await GetBudgetPage(browser, Blazor.Constants.Transaction.CataloguePath);
        await WaitForSideBar(page);
        await WaitForEndpointsToFinish(page);
        await TakeScreenshot(page, "transactions");
    }

    private static async Task WaitForSideBar(IPage home)
    {
        await home.Locator(".sidebar").WaitForAsync();
    }

    private static async Task WaitForEndpointsToFinish(IPage home)
    {
        await home.WaitForResponseAsync("**");
    }

    private static async Task ClickGreenButton(IPage page)
    {
        await page.Locator(".btn.btn-success").ClickAsync();
    }

    private async Task TakeScreenshot(IPage page, string title)
    {
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = Path.Combine(_imageFolder, $"{title}.png"),
            Type = ScreenshotType.Png
        });
    }

    private async Task<IPage> GetBudgetPage(IBrowser browser, string route)
    {
        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
           BaseURL = "https://localhost:7098"
        });
        var page = await context.NewPageAsync();
        await page.GotoAsync(route);
        return page;
    }

    private async Task<IBrowser> CreateBrowser()
    {
        return await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
    }
}