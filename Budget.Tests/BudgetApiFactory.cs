using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Budget.Tests;

internal class BudgetApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
        });
    }
}

public class BudgetApiFactoryFixture : IDisposable
{
    private readonly BudgetApiFactory _factory;

    public BudgetApiFactoryFixture()
    {
        _factory = new BudgetApiFactory();
    }

    public HttpClient CreateClient()
    {
        return _factory.CreateClient();
    }

    public void Dispose()
    {
        _factory.Dispose();
    }
}