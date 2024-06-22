namespace LeagueBoss.Api.Users.Endpoints;

using Application.Users.Commands.CreateUser;
using Domain.Users;

public class CreateUserEndpoint : Endpoint<CreateUserCommand, UserId>
{
    private readonly IMediator _mediator;

    public CreateUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override void Configure()
    {
        Post("/users/create");
        Summary(s => s.Summary = "Create a single user");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserCommand req, CancellationToken ct)
    {
        var userId = await _mediator.Send(req, ct);

        if (!userId.IsSuccess)
        {
            await SendResultAsync(Results.BadRequest(userId.Errors));
            return;
        }

        await SendOkAsync(userId.Value, ct);
    }
}