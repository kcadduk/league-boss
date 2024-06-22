namespace LeagueBoss.Infrastructure.Tests.Integration.Persistence;

using Infrastructure.Persistence;
using Infrastructure.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer;
    public ServiceProvider Services { get; } = null!;

    public string ConnectionString { get; private set; } = null!;

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
    }

    private void BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection()
            .ConfigureInfrastructureServices();

        serviceCollection.RemoveAll(typeof(DbContextOptions));
        serviceCollection.AddSingleton<DbContextOptions>(_ =>
            new DbContextOptionsBuilder()
                .UseSqlServer(ConnectionString)
                #if DEBUG
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)
                #endif
                .Options);
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.DisposeAsync();
    }
}

[CollectionDefinition(nameof(DatabaseFixture))]
public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
{
}