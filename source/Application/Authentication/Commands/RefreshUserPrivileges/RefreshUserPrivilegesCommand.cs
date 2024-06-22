namespace LeagueBoss.Application.Authentication.Commands.RefreshUserPrivileges;

using Domain.Users;
using MediatR;

public record RefreshUserPrivilegesCommand(UserId UserId) : IRequest<AuthenticatedUserDto>;

public class RefreshUserPrivilegesCommandHandler : IRequestHandler<RefreshUserPrivilegesCommand, AuthenticatedUserDto>
{

    public Task<AuthenticatedUserDto> Handle(RefreshUserPrivilegesCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AuthenticatedUserDto()
        {
            UserId = request.UserId
        });
    }
}