namespace LeagueBoss.Application.Tests.Unit;

using Authentication.Commands.RefreshUserPrivileges;
using Domain.Users;

public class RefreshUserPrivilegesCommandHandlerTests
{
    private readonly RefreshUserPrivilegesCommandHandler _sut;

    public RefreshUserPrivilegesCommandHandlerTests()
    {
        _sut = new RefreshUserPrivilegesCommandHandler();
    }
    [Fact]
    public async Task HandleShould_ReturnAuthenticatedUserDto_WhenCalled()
    {
        // Arrange
        var command = new RefreshUserPrivilegesCommand(UserId.New());
        
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        await Verify(res, Verify.Settings);
    }
}