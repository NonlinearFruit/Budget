using System;
using System.Net.Http;
using System.Threading.Tasks;
using Budget.Shared;
using Newtonsoft.Json;
using Xunit;

namespace Budget.Tests;

public class ComponentTests : IClassFixture<BudgetApiFactoryFixture>
{
    private readonly HttpClient _client;

    public ComponentTests(BudgetApiFactoryFixture factory)
    {
        _client = factory.CreateClient();
    }

    [ContinuousIntegrationOnlyFact]
    public async Task swagger_endpoint_loads()
    {
        var response = await _client.GetAsync(ApiConstants.BuiltIn.GetOpenApiSpec());

        Assert.True(response.IsSuccessStatusCode);
    }

    [ContinuousIntegrationOnlyFact]
    public async Task forecast_test_endpoint_loads()
    {
        var responseMessage = await _client.GetAsync(ApiConstants.Forecast.GetTestsPath(DateTime.Now));

        Assert.True(responseMessage.IsSuccessStatusCode);
    }

    [ContinuousIntegrationOnlyFact]
    public async Task bank_account_test_endpoint_loads()
    {
        var responseMessage = await _client.GetAsync(ApiConstants.BankAccount.GetTestsPath());

        Assert.True(responseMessage.IsSuccessStatusCode);
    }
}