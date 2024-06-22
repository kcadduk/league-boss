namespace LeagueBoss.Domain.Authentication;

using Users;

public record AuthenticationRefreshTransactionToken : TransactionToken
{
    private AuthenticationRefreshTransactionToken()
    {
    }
    
    public static AuthenticationRefreshTransactionToken Create(UserId userId, DateTime tokenExpiry, string? token = default)
    {
        return new AuthenticationRefreshTransactionToken()
        {
            Token = token ?? GenerateSecureTokenString(), 
            Expires = tokenExpiry,
            UserId = userId
        };
    }
}