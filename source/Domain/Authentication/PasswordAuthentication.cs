namespace LeagueBoss.Domain.Authentication;

using Users;

[StronglyTypedId]
public readonly partial struct PasswordAuthenticationId;

public record PasswordAuthentication : IEntity<PasswordAuthenticationId>
{
    private PasswordAuthentication()
    {
    }

    public static PasswordAuthentication Create(User user, string plainTextPassword)
    {
        return new PasswordAuthentication
        {
            Password = HashedStringWithSalt.Create(plainTextPassword),
            User = user
        };
    }
    
    public PasswordAuthenticationId Id { get; init; }
    public required HashedStringWithSalt Password { get; init; }
    public required User User { get; init; }
}