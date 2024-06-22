namespace LeagueBoss.Api.Authentication.Endpoints;

using System.Security.Claims;
using Application.Authentication.Commands.AuthenticateUserWithPassword;
using FastEndpoints.Security;

public class AuthenticateUserWithPasswordEndpoint : Endpoint<AuthenticateUserWithPasswordCommand, ApiTokenResponse>
{
    private readonly IMediator _mediator;

    public AuthenticateUserWithPasswordEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/authentication/password");
        Summary(s => s.Summary = "Authenticate with an email and a password");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthenticateUserWithPasswordCommand req, CancellationToken ct)
    {
        var res = await _mediator.Send(req, ct);

        if (res.IsFailure)
        {
            await SendResultAsync(Results.BadRequest(res.Errors));
            return;
        }

        Response = await CreateTokenWith<RefreshTokenService>(res.Value.UserId.ToString(),
            u => u.FromAuthenticatedUserDto(res.Value));

        await CookieAuth.SignInAsync(p => p.FromAuthenticatedUserDto(res.Value));
    }
}