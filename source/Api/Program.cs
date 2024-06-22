using LeagueBoss.Api;
using LeagueBoss.Application;
using LeagueBoss.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
        .AddUserSecrets<Program>()
        .AddEnvironmentVariables();
    
    builder.Services
        .ConfigureWebApplicationServices(builder.Configuration)
        .ConfigureApplicationServices()
        .ConfigureInfrastructureServices();
    
    var app = builder.Build();

    app.ConfigureMiddleware();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// This line exists to support Integration Testing - Do not remove it;
public partial class Program;