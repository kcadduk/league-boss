namespace LeagueBoss.Application.Users.Commands.CreateUser;

using Domain.Users;
using Results;

public class AlreadyRegisteredException : Exception
{
    public EmailAddress EmailAddress { get; }

    public AlreadyRegisteredException(EmailAddress emailAddress)
    {
        EmailAddress = emailAddress;
    }
}