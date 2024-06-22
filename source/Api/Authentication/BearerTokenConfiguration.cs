namespace LeagueBoss.Api.Authentication;

using System.ComponentModel.DataAnnotations;

public class BearerTokenConfiguration
{
    public const string ConfigurationKey = "AuthTokens";
    
    [Required]
    public required string TokenSigningKey { get; init; }
    
    [Range(0, int.MaxValue)]
    public required int AccessTokenValidityMinutes { get; init; }
    
    [Range(0, int.MaxValue)]
    public required int RefreshTokenValidityHours { get; init; }
}