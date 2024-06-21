namespace LeagueBoss.Domain.Tests.Unit.Users;

using Domain.Users;

public class UserTests
{
    private readonly User _sut;
    
    public UserTests()
    {
        _sut = User.Create(UserName.Create("Test"), EmailAddress.Create("test@localhost"));
    }
    [Fact]
    public async Task CreateShould_CreateUser_WhenCalled()
    {
        // Arrange

        // Act 
        var res = User.Create(UserName.Create("Test"), EmailAddress.Create("test@localhost"));
        
        // Assert
        await Verify(res, Verify.Settings);
    }

    [Fact]
    public async Task WithPasswordShould_SetPassword_WhenCalled()
    {
        // Arrange
        // Act 
        _sut.WithPassword("ABC123");
        
        // Assert
        var verifySettings = Verify.Settings;
        verifySettings.ScrubMembers("Value");
        await Verify(_sut, verifySettings);
    }
}