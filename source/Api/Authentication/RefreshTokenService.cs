namespace LeagueBoss.Api.Authentication;

using System.Security.Claims;
using Application.Authentication.Commands.CreateAuthenticationRefreshTransactionToken;
using Application.Authentication.Commands.PersistAuthenticationRefreshTransactionToken;
using Application.Authentication.Commands.RefreshUserPrivileges;
using Domain.Users;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;

public class RefreshTokenService : RefreshTokenService<TokenRequest, ApiTokenResponse>
{
    private readonly IMediator _mediator;

    public RefreshTokenService(IMediator mediator, IOptions<BearerTokenConfiguration> bearerTokenConfiguration)
    {
        _mediator = mediator;

        var tokenConfiguration = bearerTokenConfiguration.Value;

        Setup(o =>
        {
            o.TokenSigningKey = tokenConfiguration.TokenSigningKey;
            o.AccessTokenValidity = TimeSpan.FromMinutes(tokenConfiguration.AccessTokenValidityMinutes);
            o.RefreshTokenValidity = TimeSpan.FromHours(tokenConfiguration.RefreshTokenValidityHours);
            o.Endpoint("/authentication/refresh",
                ep => { ep.Summary(s => s.Summary = "Bearer Refresh Token endpoint"); });
        });
    }

    public override async Task PersistTokenAsync(ApiTokenResponse response)
    {
        var userId = UserId.Parse(response.UserId);
        await _mediator.Send(new PersistAuthenticationRefreshTransactionTokenCommand(userId, response.RefreshExpiry, response.RefreshToken));
    }

    public override async Task RefreshRequestValidationAsync(TokenRequest req)
    {
        var result = await _mediator.Send(new ValidateAuthenticationRefreshTransactionTokenCommand(req.RefreshToken));
        if (result.IsFailure) AddError(x => x.RefreshToken, "Invalid Refresh Token");
    }

    public override async Task SetRenewalPrivilegesAsync(TokenRequest request, UserPrivileges privileges)
    {
        var userId = UserId.Parse(request.UserId);
        var authenticatedUser = await _mediator.Send(new RefreshUserPrivilegesCommand(userId));
        
        privileges.FromAuthenticatedUserDto(authenticatedUser);
        
        await CookieAuth.SignInAsync(p => p.FromAuthenticatedUserDto(authenticatedUser));
    }
}