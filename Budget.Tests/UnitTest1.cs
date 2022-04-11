using System.Net.Http;
using System.Threading.Tasks;
using Budget.Shared;
using Newtonsoft.Json;
using Xunit;

namespace Budget.Tests;

public class UnitTest1 : IClassFixture<BudgetApiFactoryFixture>
{
    private readonly HttpClient _client;

    public UnitTest1(BudgetApiFactoryFixture factory)
    {
        _client = factory.CreateClient();
    }

    [ContinuousIntegrationOnlyFact]
    public async Task swagger_endpoint_loads()
    {
        var responseString = await _client.GetStringAsync(ApiConstants.BuiltIn.GetOpenApiSpec());
        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);

        var endpoints = responseObject.paths;
    }
}