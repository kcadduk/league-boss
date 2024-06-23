namespace LeagueBoss.Application.Tests.Integration.Users.Commands;

using Application.Users;
using Application.Users.Commands.CreateUser;
using Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

[Collection(nameof(DatabaseFixture))]
public class CreateUserCommandTests : IClassFixture<IntegrationTestCaseFixture>, IAsyncLifetime
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

    [Fact]
    public async Task HandleShould_ReturnFailure_WhenEmailAddressExists()
    {
        // Arrange
        var command = new CreateUserCommand("test", string.Empty, "exists@localhost");
        
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsFailure.Should().BeTrue();
        await Verify(res.Errors, Verify.Settings);
    }

    public async Task InitializeAsync()
    {
        var dbContext = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        await dbContext.CreateUser();
    }

    public async Task DisposeAsync()
    {
        await _fixture.DatabaseFixture.ResetDatabase();
    }
}