using System;
using System.IO;
using System.Linq;
using Microsoft.Playwright;

namespace Budget.Tests.Utilities;

public class PlaywrightFixture : IDisposable
{
    public PlaywrightFixture()
    {
        Playwright = Microsoft.Playwright.Playwright.CreateAsync().Result;
        ScreenshotFolder = Path.Combine(GetRootDirectory().FullName, "images");
    }

    private static DirectoryInfo GetRootDirectory()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        var rootDirectory = currentDirectory;
        while (rootDirectory != null && !rootDirectory.GetFiles("README.md").Any())
            rootDirectory = rootDirectory.Parent;
        return rootDirectory ?? currentDirectory;
    }

    public IPlaywright Playwright { get; }

    public string ScreenshotFolder { get; }

    public void Dispose()
    {
        Playwright.Dispose();
    }
}