namespace LeagueBoss.Application.Tests.Integration;

public class IntegrationTestCaseFixture : IAsyncLifetime
{
    public DatabaseFixture DatabaseFixture { get; }

    public IntegrationTestCaseFixture(DatabaseFixture databaseFixture)
    {
        DatabaseFixture = databaseFixture;
    }
    
    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}