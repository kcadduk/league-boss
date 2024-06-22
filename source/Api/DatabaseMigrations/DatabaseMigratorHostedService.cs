namespace LeagueBoss.Api.DatabaseMigrations;

using Infrastructure.Persistence;

public class DatabaseMigratorHostedService : IHostedService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseMigratorHostedService(IServiceProvider serviceProvider, IWebHostEnvironment environment)
    {
        _serviceProvider = serviceProvider;
        _environment = environment;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_environment.IsEnvironment("Production")) return;

        await using var scope = _serviceProvider.CreateAsyncScope();
        var databaseMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrationHandler>();

        await databaseMigrator.Apply();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}