using System.Runtime.CompilerServices;
using Budget.Api;
using Microsoft.AspNetCore.Builder;

[assembly:InternalsVisibleTo("Budget.Tests")]

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();
startup.ConfigureServices(builder.Services);
using var app = builder.Build();
startup.Configure(app);
app.Run();