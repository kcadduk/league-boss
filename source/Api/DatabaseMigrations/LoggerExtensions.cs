namespace LeagueBoss.Api.DatabaseMigrations;

public static partial class LoggerExtensions
{
    [LoggerMessage(LogLevel.Information, "Environment is Production, Skipping Migrations")]
    public static partial void LogSkippingMigrations(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "Starting Database Migrations")]
    public static partial void LogStartingMigrations(this ILogger logger);
    
    [LoggerMessage(LogLevel.Information, "Database Migrations Completed in {Seconds}s")]
    public static partial void LogMigrationsCompleted(this ILogger logger, double seconds);
}