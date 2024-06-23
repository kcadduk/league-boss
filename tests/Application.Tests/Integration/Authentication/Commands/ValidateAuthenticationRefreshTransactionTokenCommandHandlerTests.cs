namespace LeagueBoss.Application.Tests.Integration.Authentication.Commands;

using Application.Authentication.Commands.CreateAuthenticationRefreshTransactionToken;
using Application.Users;
using Domain.Authentication;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Time;

[Collection(nameof(DatabaseFixture))]
public class ValidateAuthenticationRefreshTransactionTokenCommandHandlerTests : IClassFixture<IntegrationTestCaseFixture>, IAsyncLifetime
{
    private readonly IntegrationTestCaseFixture _fixture;
    private readonly ITimeProvider _timeProvider = Substitute.For<ITimeProvider>();
    private readonly ValidateAuthenticationRefreshTransactionTokenCommandHandler _sut;
    public AuthenticationRefreshTransactionToken ExpiredTransactionToken { get; set; } = null!;
    public AuthenticationRefreshTransactionToken ValidTransactionToken { get; set; } = null!;

    public ValidateAuthenticationRefreshTransactionTokenCommandHandlerTests(IntegrationTestCaseFixture fixture)
    {
        _fixture = fixture;
        _timeProvider.Now.Returns(_ => DateTime.UtcNow);
        var dbContext = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        _sut = new ValidateAuthenticationRefreshTransactionTokenCommandHandler(dbContext, _timeProvider);
    }

    [Fact]
    public async Task HandleShould_ReturnFailure_WhenTokenIsExpired()
    {
        // Arrange
        var command = new ValidateAuthenticationRefreshTransactionTokenCommand(ExpiredTransactionToken.Token);
        
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShould_ReturnFailure_WhenTokenStringDoesNotExist()
    {
        // Arrange
        var command = new ValidateAuthenticationRefreshTransactionTokenCommand(Guid.NewGuid().ToString());
        
        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task HandleShould_ReturnSuccess_WhenTokenIsValid()
    {
        // Arrange
        var command = new ValidateAuthenticationRefreshTransactionTokenCommand(ValidTransactionToken.Token);

        // Act 
        var res = await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        res.IsSuccess.Should().BeTrue();
    }
    
    
    public async Task InitializeAsync()
    {
        var dbContext = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        var user = await dbContext.CreateUser();
        ExpiredTransactionToken =
            await dbContext.CreateAuthenticationRefreshTransactionToken(user, DateTime.MinValue,
                Guid.NewGuid().ToString());

        ValidTransactionToken =
            await dbContext.CreateAuthenticationRefreshTransactionToken(user, DateTime.UtcNow.AddHours(1),
                Guid.NewGuid().ToString());
    }



    public async Task DisposeAsync()
    {
        await _fixture.DatabaseFixture.ResetDatabase();
    }
}