using System.Diagnostics.CodeAnalysis;
using Budget.Blazor.Utilities;

namespace Budget.Blazor.Tests.TestDoubles.Utilities;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class NavigationItem_TestDouble : INavigationItem
{
    public string Path { get; }
    public string Title { get; set; }
    public string Icon { get; }
}