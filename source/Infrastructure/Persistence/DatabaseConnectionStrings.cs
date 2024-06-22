namespace LeagueBoss.Infrastructure.Persistence;

using System.ComponentModel.DataAnnotations;

public class DatabaseConnectionStrings
{
    public const string ConfigurationKey = "ConnectionStrings";
    
    [Required]
    public required string SqlServer { get; init; }
}