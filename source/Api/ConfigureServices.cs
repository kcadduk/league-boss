namespace LeagueBoss.Api;

using System.Reflection;
using Authentication;
using DatabaseMigrations;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Serilog;

public static class ConfigureServices
{
    private const string JtwOrCookie = "Jwt_Or_Cookie";
    internal static IServiceCollection ConfigureWebApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.ConfigureFastEndpoints()
            .ConfigureAuthentication(configuration)
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

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var bearerConfigurationSection = configuration.GetSection(BearerTokenConfiguration.ConfigurationKey);
        serviceCollection
            .Configure<BearerTokenConfiguration>(bearerConfigurationSection)
            .AddOptions<BearerTokenConfiguration>()
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var bearerConfiguration = bearerConfigurationSection.Get<BearerTokenConfiguration>() 
                                  ?? throw new NotSupportedException("Bearer Token Configuration is Required");
        
        
        serviceCollection.AddAuthenticationCookie(validFor: TimeSpan.FromDays(1), o =>
            {
                o.Cookie.Name = "LeagueBossCookie";
                o.SlidingExpiration = true;
                o.Events.OnRedirectToLogin = c =>
                {
                    c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            })
            .AddAuthenticationJwtBearer(options => options.SigningKey = bearerConfiguration.TokenSigningKey)
            .AddAuthentication(o =>
            {
                o.DefaultScheme = JtwOrCookie;
                o.DefaultAuthenticateScheme = JtwOrCookie;
            })
            .AddPolicyScheme(JtwOrCookie, JtwOrCookie, o =>
            {
                o.ForwardDefaultSelector = ctx =>
                {
                    if (ctx.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader) &&
                        authHeader.FirstOrDefault()?.StartsWith("Bearer ") is true)
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }

                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            });
        
        serviceCollection.AddAuthorization();

        return serviceCollection;
    }
    private static IServiceCollection ConfigureFastEndpoints(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddFastEndpoints()
            .SwaggerDocument();
        
        return serviceCollection;
    }
}