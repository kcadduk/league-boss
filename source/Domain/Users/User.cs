namespace LeagueBoss.Domain.Users;

using Authentication;

[StronglyTypedId]
public readonly partial struct UserId;
public record User : Entity, IEntity<UserId>
{
    private User() {}
    
    public UserId Id { get; init; }
    public required UserName Name { get; init; }
    public EmailAddress EmailAddress { get; private set; } = null!;
    public PasswordAuthentication? PasswordAuthentication { get; private set; }

    public static User Create(UserName name, EmailAddress emailAddress)
    {
        return new User
        {
            Name = name,
            EmailAddress = emailAddress
        };
    }

    public void WithPassword(string plainTextPassword)
    {
        PasswordAuthentication = PasswordAuthentication.Create(this, plainTextPassword);
    }
}