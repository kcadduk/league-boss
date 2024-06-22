namespace LeagueBoss.Api;

using System.Reflection;
using DatabaseMigrations;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

public static class ConfigureServices
{
    internal static IServiceCollection ConfigureWebApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.ConfigureFastEndpointsWithAuth()
            .AddCors()
            .AddSerilog((sp, serilog) =>
            {
                serilog
                    .WriteTo.Console()
                    .ReadFrom.Services(sp);
            });

        serviceCollection.AddMediator();
        serviceCollection.AddHostedService<DatabaseMigratorHostedService>();

        serviceCollection
            .Configure<DatabaseConnectionStrings>(configuration.GetSection(DatabaseConnectionStrings.ConfigurationKey))
            .AddOptions<DatabaseConnectionStrings>()
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddMediator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(o => { o.RegisterServicesFromAssembly(Assembly.Load("LeagueBoss.Application")); });
        return serviceCollection;
    }

    private static IServiceCollection ConfigureFastEndpointsWithAuth(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFastEndpoints()
            .SwaggerDocument();
        
        serviceCollection
            .AddAuthentication(auth => { auth.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        serviceCollection.AddAuthorization();
        
        return serviceCollection;
    }
}