using FluentAssertions;
using LeagueBoss.Domain.Users;

namespace LeagueBoss.Domain.Tests.Unit.Users;

public class EmailAddressTests
{
    [Fact]
    public void CreateShould_ReturnEmailAddress_WhenValidEmailProvided()
    {
        // Act
        var email = "test@example.com";
        var result = EmailAddress.Create(email);

        // Assert
        result.Address.Should().Be(email);
    }

    [Fact]
    public void CreateShould_ThrowInvalidEmailAddressException_WhenInvalidEmailProvided()
    {
        // Act
        var email = "invalid";

        // Assert
        Action act = () => EmailAddress.Create(email);
        act.Should().Throw<InvalidEmailAddressException>()
            .WithMessage("The specified string is not in the form required for an e-mail address.");
    }

    [Fact]
    public void CreateShould_ThrowInvalidEmailAddressException_WhenNullEmailProvided()
    {
        // Act
        string email = null!;

        // Assert
        Action act = () => EmailAddress.Create(email);
        act.Should().Throw<InvalidEmailAddressException>()
            .WithMessage("Value cannot be null. (Parameter 'address')");
    }

    [Fact]
    public void TryCreate_ShouldReturnTrue_WhenEmailAddressIsValid()
    {
        // Arrange
        string validEmail = "test@example.com";

        // Act
        var result = EmailAddress.TryCreate(validEmail, out var emailAddress);

        // Assert
        result.Should().BeTrue();
        emailAddress.Should().NotBeNull();
        emailAddress!.Address.Should().Be(validEmail);
    }

    [Fact]
    public void TryCreate_ShouldReturnFalse_WhenEmailAddressIsInvalid()
    {
        // Arrange
        string invalidEmail = "invalid email";

        // Act
        var result = EmailAddress.TryCreate(invalidEmail, out var emailAddress);

        // Assert
        result.Should().BeFalse();
        emailAddress.Should().BeNull();
    }
}