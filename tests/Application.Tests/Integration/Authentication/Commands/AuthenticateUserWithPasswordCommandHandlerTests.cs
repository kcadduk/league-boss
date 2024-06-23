namespace LeagueBoss.Application.Tests.Integration.Authentication.Commands;

using Application.Authentication.Commands.AuthenticateUserWithPassword;
using Application.Users;
using Domain.Users;
using Microsoft.Extensions.DependencyInjection;

[Collection(nameof(DatabaseFixture))]
public class AuthenticateUserWithPasswordCommandHandlerTests : IClassFixture<IntegrationTestCaseFixture>, IAsyncLifetime
{
    private readonly IntegrationTestCaseFixture _fixture;
    public User User { get; private set; } = null!;
    public const string PlainTextPassword = "Password123";
    private readonly AuthenticateUserWithPasswordCommandHandler _sut;
    
    public AuthenticateUserWithPasswordCommandHandlerTests(IntegrationTestCaseFixture fixture)
    {
        _fixture = fixture;
        var dbContext = fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        _sut = new AuthenticateUserWithPasswordCommandHandler(dbContext);
    }

    [Fact]
    public async Task HandleShould_ReturnSuccess_WhenUserIsAuthenticated()
    {
        // Arrange
        var command = new AuthenticateUserWithPasswordCommand(User.EmailAddress.Address, PlainTextPassword);
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsSuccess.Should().BeTrue();
        await Verify(res.Value, Verify.Settings);
    }

    [Fact]
    public async Task HandleShould_ReturnFailure_WhenUserHasIncorrectPassword()
    {
        // Arrange
        var command = new AuthenticateUserWithPasswordCommand(User.EmailAddress.Address, "IncorrectPassword");
        
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsFailure.Should().BeTrue();
    }

    public async Task InitializeAsync()
    {
        var dbContext = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        User = await dbContext.CreateUser();
        User.WithPassword(PlainTextPassword);
        await dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _fixture.DatabaseFixture.ResetDatabase();
    }
}