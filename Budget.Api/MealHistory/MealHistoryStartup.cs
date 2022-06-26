namespace Budget.Api.MealHistory;

public static class MealHistoryStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IMealHistoryContext, MealHistoryContext>();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IMealHistoryContext>()?.Migrate();
    }
}