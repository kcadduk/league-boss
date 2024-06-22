namespace LeagueBoss.Api.Authentication;

using System.Security.Claims;
using Application.Authentication.Commands;
using Application.Authentication.Commands.AuthenticateUserWithPassword;

public static class AuthenticatedUserDtoExtensions
{
    internal static void FromAuthenticatedUserDto(this UserPrivileges userPrivileges, AuthenticatedUserDto authenticatedUserDto)
    {
        userPrivileges.Claims.AddRange(authenticatedUserDto.Claims());
    }

    private static IEnumerable<Claim> Claims(this AuthenticatedUserDto authenticatedUserDto)
    {
        return
        [
            new Claim("UserId", authenticatedUserDto.UserId.ToString())
        ];
    }
}