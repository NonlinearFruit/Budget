namespace Budget.Api.Budget;

public static class BudgetStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IBudgetContext, BudgetContext>();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IBudgetContext>()?.Migrate();
    }
}