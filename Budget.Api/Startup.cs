using System.Text.Json.Serialization;
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
        services.AddTransient<IDatabaseContext, DatabaseContext>();
        services.AddCors(option =>
        {
            option.AddPolicy("allowedOrigin",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
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
        app.ApplicationServices.GetService<IDatabaseContext>()?.Migrate();
        MealHistoryStartup.Configure(app);
    }
}