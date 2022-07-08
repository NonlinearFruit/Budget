using Budget.Blazor.MealHistory;
using Budget.Blazor.Utilities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Budget.Blazor;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var baseApiUrl = builder.HostEnvironment.BaseAddress;
        var uriBuilder = new UriBuilder(baseApiUrl)
        {
            Port = 7162
        };
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = uriBuilder.Uri });
        builder.Services.AddTransient<INavigationItem, BankAccountCatalogue>();
        builder.Services.AddTransient<INavigationItem, CategoryCatalogue>();
        builder.Services.AddTransient<INavigationItem, CheckCatalogue>();
        builder.Services.AddTransient<INavigationItem, ForecastCatalogue>();
        builder.Services.AddTransient<INavigationItem, Home>();
        builder.Services.AddTransient<INavigationItem, MealCatalogue>();
        builder.Services.AddTransient<INavigationItem, TagCatalogue>();
        builder.Services.AddTransient<INavigationItem, TransactionCatalogue>();

        await builder.Build().RunAsync();
    }
}
