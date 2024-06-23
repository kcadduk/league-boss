namespace LeagueBoss.Api.Tests.Integration.Authentication;

using Application.Authentication.Commands;
using Application.Authentication.Commands.CreateAuthenticationRefreshTransactionToken;
using Application.Authentication.Commands.RefreshUserPrivileges;
using Application.Results;
using Domain.Users;
using FastEndpoints.Security;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

[Collection(nameof(WebApplicationFixture))]
public class RefreshTokenServiceTests
{
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;

    public RefreshTokenServiceTests(WebApplicationFixture fixture)
    {
        _httpClient = fixture.Factory.CreateClient();
        _mediator = fixture.Factory.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task HandleShould_Return400BadRequest_WhenRefreshTokenIsNotValid()
    {
        // Arrange
        var tokenRequest = new TokenRequest
        {
            UserId = string.Empty,
            RefreshToken = string.Empty
        };
        _mediator.Send(Arg.Any<ValidateAuthenticationRefreshTransactionTokenCommand>(), Arg.Any<CancellationToken>())
            .Returns(Result.Fail());
        
        // Act 
        var res = await _httpClient.PostAsJsonAsync("/authentication/refresh", tokenRequest);
        
        // Assert
        res.Should().Be400BadRequest();
    }

    [Fact]
    public async Task HandleShould_Return200Ok_WhenRefreshTokenIsValid()
    {
        // Arrange
        var userId = UserId.New();
        var tokenRequest = new TokenRequest
        {
            UserId = userId.ToString(),
            RefreshToken = string.Empty
        };
        
        _mediator.Send(Arg.Any<ValidateAuthenticationRefreshTransactionTokenCommand>(),
            Arg.Any<CancellationToken>())
            .Returns(Result.Ok());

        _mediator.Send(Arg.Any<RefreshUserPrivilegesCommand>(), Arg.Any<CancellationToken>())
            .Returns(new AuthenticatedUserDto
            {
                UserId = userId
            });
        
        // Act 
        var res = await _httpClient.PostAsJsonAsync("/authentication/refresh", tokenRequest);
        
        // Assert
        res.Should().Be200Ok();
    }
}