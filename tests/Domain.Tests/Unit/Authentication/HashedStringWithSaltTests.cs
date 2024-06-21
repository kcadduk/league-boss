namespace LeagueBoss.Domain.Tests.Unit.Authentication;

using Domain.Authentication;
using FluentAssertions;

public class HashedStringWithSaltTests
{
    [Fact]
    public void CreateShould_CreateHashedAndSaltedPassword_WhenCalled()
    {
        // Arrange
        // Act
        var res = HashedStringWithSalt.Create("Password123");
        
        // Assert
        res.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void CreateShould_Throw_WhenPlainTextStringIsEmpty()
    {
        // Arrange
        // Act
        var act = () => HashedStringWithSalt.Create("");
        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void IsMatchShould_ReturnTrue_WhenCorrectPasswordIsInput()
    {
        // Arrange
        var sut = HashedStringWithSalt.Create("Password123");
        
        // Act
        var res = sut.IsMatch("Password123");
        
        // Assert
        res.Should().BeTrue();
    }

    [Fact]
    public void IsMatchShould_ReturnFalse_WhenIncorrectPasswordIsInput()
    {
        // Arrange
        var sut = HashedStringWithSalt.Create("Password123");
        
        // Act
        var res = sut.IsMatch("IncorrectPassword");
        
        // Assert
        res.Should().BeFalse();
    }

    [Fact]
    public void TryParseShould_ParseValidString_WhenCalled()
    {
        // Arrange
        var password = HashedStringWithSalt.Create("Password123");
        
        // Act 
        var res = HashedStringWithSalt.TryParse(password.ToString(), out var hash);
        
        // Assert
        res.Should().BeTrue();
        hash.Should().BeEquivalentTo(password);
    }
} 
    