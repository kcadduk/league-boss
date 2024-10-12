namespace LeagueBoss.Api;

using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

public static class ConfigureServices
{
    internal static IServiceCollection ConfigureWebApplicationServices(this IServiceCollection serviceCollection)
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
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddMediator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(o => { o.RegisterServicesFromAssembly(Assembly.Load("LeagueBoss.Application")); });
        return serviceCollection;
    }

    private static IServiceCollection ConfigureFastEndpointsWithAuth(this IServiceCollection serviceCollection)
    {
        // serviceCollection.AddFastEndpoints()
        //     .SwaggerDocument();
        
        serviceCollection
            .AddAuthentication(auth => { auth.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        serviceCollection.AddAuthorization();
        
        return serviceCollection;
    }
}