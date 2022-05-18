using System;
using Xunit;

namespace Budget.Tests;

public class ContinuousIntegrationOnlyFactAttribute : FactAttribute {
    private const string IncludeContinuousIntegrationTests = "IncludeContinuousIntegrationTests";

    private static readonly bool ShouldIncludeContinuousIntegrationTests =
        bool.Parse(Environment.GetEnvironmentVariable(IncludeContinuousIntegrationTests) ?? $"{false}");

    public ContinuousIntegrationOnlyFactAttribute() {
        Skip = ShouldIncludeContinuousIntegrationTests
            ? null
            : $"Run with `dotnet test -e {IncludeContinuousIntegrationTests}={true}`";
    }
}
