using Budget.Blazor.Tests.TestDoubles.Utilities;
using Budget.Blazor.Utilities;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Budget.Blazor.Tests.Tests.Utilities;

public class NavMenuTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void renders_all_navigation_items(int countOfItems)
    {
        var context = new TestContext();
        for (var i = 0; i < countOfItems; i++)
            context.Services.AddSingleton<INavigationItem>(new NavigationItem_TestDouble());

        var menu = context.RenderComponent<NavMenu>();
        var menuItems = menu.FindAll(".nav-item");

        Assert.Equal(countOfItems, menuItems.Count);
    }

    [Fact]
    public void shows_the_title_of_the_menu_item()
    {
        var title = "Home";
        var context = new TestContext();
        context.Services.AddSingleton<INavigationItem>(new NavigationItem_TestDouble
        {
            Title = title
        });

        var menu = context.RenderComponent<NavMenu>();
        var menuItems = menu.Find(".nav-item");

        Assert.Contains(title, menuItems.TextContent);
    }
}