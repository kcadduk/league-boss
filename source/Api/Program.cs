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

    builder.AddServiceDefaults();

    builder.Services
        .ConfigureWebApplicationServices()
        .ConfigureApplicationServices()
        .ConfigureInfrastructureServices();

    builder.ConfigureCosmosDb();
    
    var app = builder.Build();

    app.ConfigureMiddleware();

    app.MapDefaultEndpoints();

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