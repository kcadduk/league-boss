namespace LeagueBoss.Application.Tests.Integration;

using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Users;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Respawn;
using Testcontainers.MsSql;

public class DatabaseFixture : IAsyncLifetime, IServiceProvider
{
    private readonly MsSqlContainer _sqlContainer;
    private ServiceProvider Services { get; set; } = null!;
    private Respawner Respawn { get; set; } = null!;
    public string ConnectionString { get; private set; } = null!;

    public async Task ResetDatabase()
    {
        await Respawn.ResetAsync(ConnectionString);
    }
    public DatabaseFixture()
    {
        _sqlContainer = new MsSqlBuilder()
            .WithPassword("password@123")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
        BuildServiceProvider();
        var databaseMigrator = Services.GetRequiredService<IDatabaseMigrationHandler>();
        await databaseMigrator.Apply();
        
        Respawn = await Respawner.CreateAsync(ConnectionString);

    }

    private void BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection()
            .ConfigureInfrastructureServices();

        ConnectionString = new SqlConnectionStringBuilder(_sqlContainer.GetConnectionString())
        {
            InitialCatalog = "LeagueBossApplicationIntegrationTests"
        }.ConnectionString;
        
        serviceCollection.RemoveAll(typeof(IOptions<DatabaseConnectionStrings>));
        serviceCollection.AddSingleton(Options.Create(new DatabaseConnectionStrings
        {
            SqlServer = ConnectionString
        }));
        
        serviceCollection.RemoveAll(typeof(DbContextOptions<UsersDbContext>));
        serviceCollection.AddSingleton<DbContextOptions<UsersDbContext>>(_ =>
            new DbContextOptionsBuilder<UsersDbContext>()
                .UseSqlServer(ConnectionString)
#if DEBUG
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)
#endif
                .Options);

        Services = serviceCollection.BuildServiceProvider();
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.DisposeAsync();
    }

    public object? GetService(Type serviceType)
    {
        return Services.GetService(serviceType);
    }
}

[CollectionDefinition(nameof(DatabaseFixture))]
public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
{
}