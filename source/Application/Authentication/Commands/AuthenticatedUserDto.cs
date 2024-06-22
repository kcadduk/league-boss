namespace LeagueBoss.Application.Authentication.Commands;

using LeagueBoss.Domain.Users;

public record AuthenticatedUserDto
{
    public required UserId UserId { get; init; }
}