namespace LeagueBoss.Api.DatabaseMigrations;

using System.Diagnostics;
using FluentMigrator.Runner;
using Infrastructure.Persistence;

public class DatabaseMigratorHostedService : IHostedService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<DatabaseMigratorHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigratorHostedService(IServiceProvider serviceProvider, IWebHostEnvironment environment, ILogger<DatabaseMigratorHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _environment = environment;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_environment.IsEnvironment("Production"))
        {
            _logger.LogSkippingMigrations();
            return;
        };

        await using var scope = _serviceProvider.CreateAsyncScope();
        var databaseMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrationHandler>();

        var stopWatch = Stopwatch.StartNew();
        
        _logger.LogStartingMigrations();
        
        await databaseMigrator.Apply();
        
        _logger.LogMigrationsCompleted(stopWatch.Elapsed.TotalSeconds);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}