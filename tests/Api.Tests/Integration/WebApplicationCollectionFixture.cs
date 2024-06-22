namespace LeagueBoss.Api.Tests.Integration;

using Microsoft.AspNetCore.Mvc.Testing;

public class WebApplicationFixture 
{
    public LeagueBossWebApplicationFactory Factory { get; } = new ();
}

[CollectionDefinition(nameof(WebApplicationFixture))]
public class WebApplicationCollectionFixture : ICollectionFixture<WebApplicationFixture>;
