namespace LeagueBoss.Api.Tests.Integration.Authentication.Endpoints;

using Application.Authentication.Commands;
using Application.Authentication.Commands.AuthenticateUserWithPassword;
using Domain.Users;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

[Collection(nameof(WebApplicationFixture))]
public class AuthenticateUserWithPasswordEndpointTests : IClassFixture<WebApplicationFixture>
{
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;
    
    public AuthenticateUserWithPasswordEndpointTests(WebApplicationFixture fixture)
    {
        _httpClient = fixture.Factory.CreateClient();
        _mediator = fixture.Factory.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task HandleShould_Return200Ok_WhenCalledWithValidCommand()
    {
        // Arrange
        _mediator.Send(Arg.Any<AuthenticateUserWithPasswordCommand>(), Arg.Any<CancellationToken>())
            .Returns(new AuthenticatedUserDto
            {
                UserId = UserId.New()
            });
        
        var command = new AuthenticateUserWithPasswordCommand("user@localhost", "ABC123");
        
        // Act 
        var res = await _httpClient.PostAsJsonAsync("/authentication/password", command);
        
        // Assert
        res.Should().Be200Ok();
    }
}