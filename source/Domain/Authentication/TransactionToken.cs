namespace LeagueBoss.Domain.Authentication;

using System.Security.Cryptography;
using Users;

[StronglyTypedId]
public readonly partial struct TransactionTokenId;
public abstract record TransactionToken : IEntity<TransactionTokenId>
{
    private protected TransactionToken(){}
    
    public TransactionTokenId Id { get; init; }
    public required UserId UserId { get; init; }
    public required string Token { get; init; }
    public required DateTime Expires { get; init; }
    
    protected static string GenerateSecureTokenString()
    {
        var randomNumber = new byte[32];
        RandomNumberGenerator.Fill(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}