namespace LeagueBoss.Application.Users.Commands.CreateUser;

using Domain.Users;
using Results;

public record AlreadyRegisteredException : Result<UserId>
{
    public EmailAddress EmailAddress { get; }

    public AlreadyRegisteredException(EmailAddress emailAddress)
    {
        EmailAddress = emailAddress;
    }
}