namespace LeagueBoss.Infrastructure;

using Application.Time;
using Application.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Users;
using Time;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDatabaseMigrationHandler, DatabaseMigrationHandler>();
        serviceCollection.ConfigureEntityFramework();
        serviceCollection.AddTimeProvider();
        return serviceCollection;
    }

    private static IServiceCollection ConfigureEntityFramework(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<IUsersDbContext, UsersDbContext>((provider, builder) =>
        {
            var databaseConnectionStrings = provider.GetRequiredService<IOptions<DatabaseConnectionStrings>>().Value;
            builder.UseSqlServer(databaseConnectionStrings.SqlServer);

#if DEBUG
            builder.EnableSensitiveDataLogging();
#endif
        });
        return serviceCollection;
    }

    private static IServiceCollection AddTimeProvider(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ITimeProvider, TimeProvider>();
        return serviceCollection;
    }
}