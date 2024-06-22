namespace LeagueBoss.Application.Authentication.Commands.AuthenticateUserWithPassword;

using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Results;
using Users;

public record AuthenticateUserWithPasswordCommand(string EmailAddress, string PlainTextPassword) : IRequest<Result<AuthenticatedUserDto>>;

public class AuthenticateUserWithPasswordCommandHandler : IRequestHandler<AuthenticateUserWithPasswordCommand, Result<AuthenticatedUserDto>>
{
    private readonly IUsersDbContext _usersDbContext;

    public AuthenticateUserWithPasswordCommandHandler(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    public async Task<Result<AuthenticatedUserDto>> Handle(AuthenticateUserWithPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!EmailAddress.TryCreate(request.EmailAddress, out var emailAddress))
        {
            return new UnauthorizedException();
        }

        var user = await _usersDbContext.GetUserFromEmailAddress(emailAddress, cancellationToken: cancellationToken);
        if (user?.PasswordAuthentication is null)
        {
            return new UnauthorizedException();
        }

        if (!user.PasswordAuthentication.Password.IsMatch(request.PlainTextPassword))
        {
            return new UnauthorizedException();
        }

        return new AuthenticatedUserDto
        {
            UserId = user.Id
        };
    }
}