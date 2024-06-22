namespace LeagueBoss.Infrastructure.Persistence;

public class DatabaseConnectionStrings
{
    public const string ConfigurationKey = "ConnectionStrings";
    public required string SqlServer { get; init; }
}