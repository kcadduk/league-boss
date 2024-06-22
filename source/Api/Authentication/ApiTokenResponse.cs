namespace LeagueBoss.Api.Authentication;

using FastEndpoints.Security;

public class ApiTokenResponse : TokenResponse
{
    public int AccessValiditySeconds => (int)AccessExpiry.Subtract(DateTime.UtcNow).TotalSeconds;
    public int RefreshValiditySeconds => (int)RefreshExpiry.Subtract(DateTime.UtcNow).TotalSeconds;
}