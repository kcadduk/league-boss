namespace LeagueBoss.Infrastructure.Persistence;

using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public class DatabaseMigrationHandler : IDatabaseMigrationHandler
{
    private readonly DatabaseConnectionStrings _connectionStrings;
    
    public DatabaseMigrationHandler(IOptions<DatabaseConnectionStrings> connectionStringsOptions)
    {
        _connectionStrings = connectionStringsOptions.Value;
    }

    public async Task Apply()
    {
        var runner = CreateServices(_connectionStrings.SqlServer)
            .GetRequiredService<IMigrationRunner>();

        await EnsureDatabaseCreated();
        
        runner.MigrateUp();
    }

    private async Task EnsureDatabaseCreated()
    {
        var sqlConnectionString = new SqlConnectionStringBuilder(_connectionStrings.SqlServer);
        var newDatabaseName = sqlConnectionString.InitialCatalog;
        sqlConnectionString.InitialCatalog = "master";

        await using var connection = new SqlConnection(sqlConnectionString.ConnectionString);
        await using var command = connection.CreateCommand();

        command.CommandText = $"""
                               IF NOT EXISTS(SELECT * FROM sys.databases WHERE NAME='{newDatabaseName}') 
                               BEGIN 
                                   CREATE DATABASE [{newDatabaseName}]; 
                               END
                               """;
        
        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    private static ServiceProvider CreateServices(string connectionString)
    {

        
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb =>
                rb.AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider();
    }
}