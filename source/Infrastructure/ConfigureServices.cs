namespace LeagueBoss.Infrastructure;

using Application.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Users;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDatabaseMigrationHandler, DatabaseMigrationHandler>();
        serviceCollection.ConfigureEntityFramework();
        return serviceCollection;
    }

    private static IServiceCollection ConfigureEntityFramework(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<DbContextOptions>(provider =>
        {
            var databaseConnectionStrings = provider.GetRequiredService<IOptions<DatabaseConnectionStrings>>().Value;
            
            return new DbContextOptionsBuilder()
                .UseSqlServer(databaseConnectionStrings.SqlServer)
                .Options;
        });
        serviceCollection.AddDbContext<IUsersDbContext, UsersDbContext>();
        return serviceCollection;
    }
}