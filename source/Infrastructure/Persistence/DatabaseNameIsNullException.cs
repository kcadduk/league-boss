namespace LeagueBoss.Infrastructure.Persistence;

internal class DatabaseNameIsNullException : Exception
{
    public string? InitialCatalog { get; }

    public DatabaseNameIsNullException(string? initialCatalog)
    {
        InitialCatalog = initialCatalog;
    }
}