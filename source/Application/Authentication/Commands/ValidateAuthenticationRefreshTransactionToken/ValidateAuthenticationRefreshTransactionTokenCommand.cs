namespace LeagueBoss.Application.Authentication.Commands.CreateAuthenticationRefreshTransactionToken;

using AuthenticateUserWithPassword;
using MediatR;
using Results;
using Time;
using Users;

public record ValidateAuthenticationRefreshTransactionTokenCommand(string Token)
    : IRequest<Result>;

public class ValidateAuthenticationRefreshTransactionTokenCommandHandler : IRequestHandler<ValidateAuthenticationRefreshTransactionTokenCommand, Result>
{
    private readonly IUsersDbContext _usersDbContext;
    private readonly ITimeProvider _timeProvider;

    public ValidateAuthenticationRefreshTransactionTokenCommandHandler(IUsersDbContext usersDbContext, ITimeProvider timeProvider)
    {
        _usersDbContext = usersDbContext;
        _timeProvider = timeProvider;
    }
    
    public async Task<Result> Handle(ValidateAuthenticationRefreshTransactionTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _usersDbContext.GetNonExpiredTransactionToken(request.Token, _timeProvider);

        if (token is null)
        {
            return new UnauthorizedException();
        }

        _usersDbContext.Remove(token);
        await _usersDbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Ok();
    }
}