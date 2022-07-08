namespace Budget.Api.Meal;

public static class MealHistoryStartup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IMealContext, MealContext>();
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.ApplicationServices.GetService<IMealContext>()?.Migrate();
    }
}