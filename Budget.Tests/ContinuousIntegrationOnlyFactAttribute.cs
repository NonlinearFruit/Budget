using System;
using Xunit;

namespace Budget.Tests;

public class ContinuousIntegrationOnlyFactAttribute : FactAttribute {
    // Multiple inheritance with this https://stackoverflow.com/questions/38503146/combining-multiple-attributes-to-a-single-attribute-merge-attributes/38504552#38504552
    private const string IncludeContinuousIntegrationTests = "IncludeContinuousIntegrationTests";

    private static readonly bool ShouldIncludeContinuousIntegrationTests =
        bool.Parse(Environment.GetEnvironmentVariable(IncludeContinuousIntegrationTests) ?? $"{false}");

    public ContinuousIntegrationOnlyFactAttribute() {
        Skip = ShouldIncludeContinuousIntegrationTests
            ? null
            : $"Run with `dotnet test -e {IncludeContinuousIntegrationTests}={true}`";
    }
}
