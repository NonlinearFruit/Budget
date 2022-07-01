using System.Text.Json.Serialization;
using Budget.Api.Budget;
using Budget.Api.MealHistory;

namespace Budget.Api;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(option =>
        {
            option.AddPolicy("allowedOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        BudgetStartup.ConfigureServices(services);
        MealHistoryStartup.ConfigureServices(services);
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors("allowedOrigin");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
        BudgetStartup.Configure(app);
        MealHistoryStartup.Configure(app);
    }
}