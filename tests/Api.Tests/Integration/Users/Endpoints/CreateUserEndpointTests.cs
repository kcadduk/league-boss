namespace LeagueBoss.Api.Tests.Integration.Users.Endpoints;

using System.Net.Http.Json;
using Application.Users.Commands.CreateUser;
using Domain.Users;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

[Collection(nameof(WebApplicationFixture))]
public class CreateUserEndpointTests
{
    private readonly IMediator _mediator;
    private readonly HttpClient _httpClient;
    
    public CreateUserEndpointTests(WebApplicationFixture fixture)
    {
        _mediator = fixture.Factory.GetRequiredService<IMediator>();
        _httpClient = fixture.Factory.CreateClient();
    }
    
    [Fact]
    public async Task HandleShould_Send200Ok_WhenUserCreated()
    {
        // Arrange
        var command = new CreateUserCommand("test", null, "user@localhost");
        
        _mediator.Send(Arg.Any<CreateUserCommand>(), Arg.Any<CancellationToken>())
            .Returns(UserId.New());
        
        // Act 
        var res = await _httpClient.PostAsJsonAsync("/users/create", command);
        
        // Assert
        res.Should().Be200Ok();
    }

    [Fact]
    public async Task HandleShould_Return400BadRequest_WhenErrorIsReturned()
    {
        // Arrange
        var command = new CreateUserCommand("test", null, "user@localhost");
        _mediator.Send(Arg.Any<CreateUserCommand>(), Arg.Any<CancellationToken>())
            .Returns(new InvalidEmailAddressException(string.Empty));
        
        // Act 
        var res = await _httpClient.PostAsJsonAsync("/users/create", command);
        
        // Assert
        res.Should().Be400BadRequest();
    }
}