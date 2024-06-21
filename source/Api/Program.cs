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

    builder.Services
        .ConfigureWebApplicationServices()
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