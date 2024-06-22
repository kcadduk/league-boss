namespace Api.Tests.Unit;

using LeagueBoss.Api.DatabaseMigrations;
using LeagueBoss.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

public class DatabaseMigratorHostedServiceTests
{
    private readonly IDatabaseMigrationHandler _databaseMigrationHandler = Substitute.For<IDatabaseMigrationHandler>();
    private readonly IServiceProvider _serviceProvider = Substitute.For<IServiceProvider>();
    private readonly IServiceScope _serviceScope = Substitute.For<IServiceScope>();
    private readonly IServiceScopeFactory _serviceScopeFactory = Substitute.For<IServiceScopeFactory>();
    private readonly DatabaseMigratorHostedService _sut;
    private readonly IWebHostEnvironment _webHostEnvironment = Substitute.For<IWebHostEnvironment>();

    public DatabaseMigratorHostedServiceTests()
    {
        _serviceProvider.GetService(typeof(IServiceScopeFactory)).Returns(_serviceScopeFactory);
        _serviceProvider.CreateScope().Returns(new AsyncServiceScope(_serviceScope));
        _serviceScope.ServiceProvider.Returns(_serviceProvider);
        _serviceProvider.GetService(typeof(IDatabaseMigrationHandler)).Returns(_databaseMigrationHandler);

        _sut = new DatabaseMigratorHostedService(_serviceProvider, _webHostEnvironment);
    }

    [Fact]
    public async Task StartAsyncShould_CallDatabaseMigrationService_WhenCalledInDevelopment()
    {
        // Arrange
        _webHostEnvironment.EnvironmentName.Returns("Development");

        // Act 
        await _sut.StartAsync(CancellationToken.None);

        // Assert
        await _databaseMigrationHandler.Received()
            .Apply();
    }

    [Fact]
    public async Task StartAsyncShould_NotCallDatabaseMigrationService_WhenCalledInProduction()
    {
        // Arrange
        _webHostEnvironment.EnvironmentName.Returns("Production");

        // Act 
        await _sut.StartAsync(CancellationToken.None);

        // Assert
        await _databaseMigrationHandler.Received(0)
            .Apply();
    }
}