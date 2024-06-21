namespace LeagueBoss.Domain.Users;

public class InvalidEmailAddressException : Exception
{
    public string EmailAddress { get; }

    public InvalidEmailAddressException(string emailAddress, Exception? inner) : base(inner?.Message, inner)
    {
        EmailAddress = emailAddress;
    }
}