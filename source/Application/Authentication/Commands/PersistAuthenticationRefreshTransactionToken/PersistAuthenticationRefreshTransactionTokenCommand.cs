namespace LeagueBoss.Application.Authentication.Commands.PersistAuthenticationRefreshTransactionToken;

using Domain.Authentication;
using Domain.Users;
using MediatR;
using Time;
using Users;

public record PersistAuthenticationRefreshTransactionTokenCommand(UserId UserId, DateTime TokenExpiry, string Token) : IRequest;

public class PersistAuthenticationRefreshTransactionTokenCommandHandler : IRequestHandler<PersistAuthenticationRefreshTransactionTokenCommand>
{
    private readonly IUsersDbContext _usersDbContext;
    private readonly ITimeProvider _timeProvider;

    public PersistAuthenticationRefreshTransactionTokenCommandHandler(IUsersDbContext usersDbContext, ITimeProvider timeProvider)
    {
        _usersDbContext = usersDbContext;
        _timeProvider = timeProvider;
    }
    public async Task Handle(PersistAuthenticationRefreshTransactionTokenCommand request, CancellationToken cancellationToken)
    {
        await _usersDbContext.PruneExpiredTransactionTokens(_timeProvider);
        var token = AuthenticationRefreshTransactionToken.Create(request.UserId, request.TokenExpiry, request.Token);

        await _usersDbContext.AddAsync(token, cancellationToken);
        await _usersDbContext.SaveChangesAsync(cancellationToken);
    }
}