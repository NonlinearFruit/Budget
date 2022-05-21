using System.Collections.Generic;
using Xunit;

namespace Budget.Blazor.Tests.Tests;

public class DecimalExtensionsTests
{
    public class ToCurrency : DecimalExtensionsTests
    {
        public static IEnumerable<object[]> InputsAndOutputs() => new[]
        {
            new object[] { 0m, "$0.00" },
            new object[] { 0.1m, "$0.10" },
            new object[] { 0.01m, "$0.01" },
            new object[] { 0.001m, "$??" },
            new object[] { 1000m, "$1,000.00" },
            new object[] { -1m, "($1.00)" },
        };

        [Theory]
        [MemberData(nameof(InputsAndOutputs))]
        public void works_as_expected(decimal value, string expectation)
        {
            Assert.Equal(expectation, value.ToCurrency());
        }
    }

    public class ToColor : DecimalExtensionsTests
    {
        public static IEnumerable<object[]> InputsAndOutputs() => new[]
        {
            new object[] { 0m, "#ffffff" },
            new object[] { 0.01m, "#d8ffda" },
            new object[] { 0.001m, "#ffffd8" },
            new object[] { -1m, "#ffe4e5" },
        };

        [Theory]
        [MemberData(nameof(InputsAndOutputs))]
        public void works_as_expected(decimal value, string expectation)
        {
            Assert.Equal(expectation, value.ToColor());
        }
    }
}