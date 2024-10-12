namespace LeagueBoss.Api.Endpoints;

using FastEndpoints;

public class HelloWorldEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("hello-world");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync("Hello World!", ct);
    }
}