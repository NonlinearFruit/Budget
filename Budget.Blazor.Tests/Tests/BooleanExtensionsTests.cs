using Xunit;

namespace Budget.Blazor.Tests.Tests;

public class BooleanExtensionsTests
{
    [Theory]
    [InlineData(true, "oi-circle-check")]
    [InlineData(false, "oi-circle-x")]
    public void returns_the_expected_icon(bool value, string icon)
    {
        Assert.Equal(icon, value.ToIcon());
    }
}