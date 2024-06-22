namespace LeagueBoss.Application.Tests.Integration.Users.Commands;

using Application.Users;
using Application.Users.Commands.CreateUser;
using Microsoft.Extensions.DependencyInjection;

[Collection(nameof(DatabaseFixture))]
public class CreateUserCommandTests : IClassFixture<IntegrationTestCaseFixture>
{
    private readonly IntegrationTestCaseFixture _fixture;
    private readonly CreateUserCommandHandler _sut;
    public CreateUserCommandTests(IntegrationTestCaseFixture fixture)
    {
        _fixture = fixture;
        _sut = new CreateUserCommandHandler(_fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>());
    }

    [Fact]
    public async Task HandleShould_ReturnFailure_WhenEmailIsInvalid()
    {
        // Arrange
        var command = new CreateUserCommand("test", string.Empty, string.Empty);
        
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsFailure.Should().BeTrue();
        await Verify(res.Errors, Verify.Settings);
    }
}