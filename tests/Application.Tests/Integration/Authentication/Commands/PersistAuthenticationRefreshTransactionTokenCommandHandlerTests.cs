namespace LeagueBoss.Application.Tests.Integration.Authentication.Commands;

using Application.Authentication.Commands.PersistAuthenticationRefreshTransactionToken;
using Application.Users;
using Domain.Authentication;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Time;

[Collection(nameof(DatabaseFixture))]
public class
    PersistAuthenticationRefreshTransactionTokenCommandHandlerTests : IClassFixture<IntegrationTestCaseFixture>,
    IAsyncLifetime
{
    private readonly IntegrationTestCaseFixture _fixture;
    private readonly PersistAuthenticationRefreshTransactionTokenCommandHandler _sut;
    private readonly ITimeProvider _timeProvider = Substitute.For<ITimeProvider>();
    private User User { get; set; } = null!;
    private AuthenticationRefreshTransactionToken ExpiredTransactionToken { get; set; } = null!;

    public PersistAuthenticationRefreshTransactionTokenCommandHandlerTests(IntegrationTestCaseFixture fixture)
    {
        _fixture = fixture;
        var dbContext = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        _timeProvider.Now.Returns(_ => DateTime.UtcNow);
        _sut = new PersistAuthenticationRefreshTransactionTokenCommandHandler(dbContext, _timeProvider);
    }

    [Fact]
    public async Task HandleShould_PersistNewTransactionToken_WhenCalled()
    {
        // Arrange
        var transactionToken = Guid.NewGuid().ToString();
        var command = new PersistAuthenticationRefreshTransactionTokenCommand(User.Id, DateTime.Now, transactionToken);

        // Act 
        await _sut.Handle(command, CancellationToken.None);

        // Assert
        var verifiableTransactionToken = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>()
            .AuthenticationRefreshTransactionTokens.SingleAsync(x => x.Token == transactionToken);

        await Verify(verifiableTransactionToken, Verify.Settings);
    }

    [Fact]
    public async Task HandleShould_PruneExpiredTransactionTokens_WhenCalled()
    {
        // Arrange
        var transactionToken = Guid.NewGuid().ToString();
        var command = new PersistAuthenticationRefreshTransactionTokenCommand(User.Id, DateTime.Now, transactionToken);

        // Act 
        await _sut.Handle(command, CancellationToken.None);
        
        // Assert
        var verifiableTransactionToken = await _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>()
            .AuthenticationRefreshTransactionTokens.AnyAsync(x => x.Token == ExpiredTransactionToken.Token);

        verifiableTransactionToken.Should().BeFalse();
    }

    public async Task InitializeAsync()
    {
        var dbContext = _fixture.DatabaseFixture.GetRequiredService<IUsersDbContext>();
        User = await dbContext.CreateUser();
        ExpiredTransactionToken = await dbContext.CreateAuthenticationRefreshTransactionToken(User, DateTime.MinValue);
    }


    public async Task DisposeAsync()
    {
        await _fixture.DatabaseFixture.ResetDatabase();
    }
}