namespace LeagueBoss.Domain.Users;

using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

public record EmailAddress
{
    private EmailAddress()
    {
    }

    public static EmailAddress Create(string value)
    {
        try
        {
            var mailAddress = new MailAddress(value);
            return new EmailAddress()
            {
                Address = mailAddress.Address
            };
        }
        catch (Exception ex)
        {
            throw new InvalidEmailAddressException(value, ex);
        }
    }

    public static bool TryCreate(string value, [MaybeNullWhen(false)] out EmailAddress emailAddress)
    {
        if (MailAddress.TryCreate(value, out var mailAddress))
        {
            emailAddress = Create(value);
            return true;
        }

        emailAddress = null;
        return false;
    }

    public required string Address { get; init; }
};