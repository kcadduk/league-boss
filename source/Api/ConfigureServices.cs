namespace LeagueBoss.Api;

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
            });;
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