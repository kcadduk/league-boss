namespace LeagueBoss.Infrastructure;

using Application.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Users;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureEntityFramework();
        return serviceCollection;
    }

    private static IServiceCollection ConfigureEntityFramework(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<IUsersDbContext, UsersDbContext>((provider, builder) =>
        {
            var databaseConnectionStrings = provider.GetRequiredService<IOptions<DatabaseConnectionStrings>>().Value;
            builder.UseSqlServer(databaseConnectionStrings.UsersDbContext);
        });
        return serviceCollection;
    }
}