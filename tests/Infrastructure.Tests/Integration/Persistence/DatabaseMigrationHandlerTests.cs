namespace LeagueBoss.Infrastructure.Tests.Integration.Persistence;

using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;

public class DatabaseMigrationHandlerTests : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .WithPassword("password@123")
        .Build();

    private DatabaseMigrationHandler _sut = null!;

    [Fact]
    public async Task ApplyShould_NotThrow_WhenCalled()
    {
        // Arrange
        // Act 
        await _sut.Apply();
        // Assert
    }

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
        
        var sqlConnectionString = new SqlConnectionStringBuilder(_sqlContainer.GetConnectionString())
            {
                InitialCatalog = "league-boss-database-migration"
            };

        _sut = new DatabaseMigrationHandler(Options.Create(new DatabaseConnectionStrings()
        {
            SqlServer = sqlConnectionString.ConnectionString
        }));
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.DisposeAsync();
    }
}