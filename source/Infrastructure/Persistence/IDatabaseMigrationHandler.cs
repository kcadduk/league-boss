namespace LeagueBoss.Infrastructure.Persistence;

public interface IDatabaseMigrationHandler
{
    public Task Apply();
}