namespace LeagueBoss.Domain.Tests.Unit.Matches;

using Domain.Matches;
using FluentAssertions;
using Sports;

public class TeamMatchTests
{
    private readonly TeamMatch _sut = TeamMatch.Create(PlayerId.New(), SportId.New(), TeamId.New(), TeamId.New());

    [Theory]
    [MemberData(nameof(TeamIdConfigurationData))]
    public async Task CreateShould_CreateATeamMatch_WhenCalled(TeamId? homeTeam, TeamId? awayTeam)
    {
        // Arrange

        // Act
        var res = TeamMatch.Create(PlayerId.New(), SportId.New(), homeTeam, awayTeam);

        // Assert
        var verifySettings = Verify.Settings;
        verifySettings.UseHashedParameters(homeTeam.HasValue, awayTeam.HasValue);
        await Verify(res, verifySettings);
    }

    [Fact]
    public async Task AddMatchPlayersShould_AddMatchPlayers_WhenCalled()
    {
        // Arrange
        var homePlayers = Enumerable.Range(0, 5)
            .Select(_ => new TeamPlayer
            {
                PlayerId = PlayerId.New(), 
                TeamId = _sut.HomeTeam!.Value 
                
            });

        var awayPlayers = Enumerable.Range(0, 5)
            .Select(_ => new TeamPlayer()
            {
                PlayerId = PlayerId.New(),
                TeamId = _sut.AwayTeam!.Value
            });

        // Act
        _sut.AddMatchPlayers([..homePlayers, ..awayPlayers]);

        // Assert
        await Verify(_sut, Verify.Settings);
    }
    
    [Fact]
    public async Task AddMatchPlayersShould_NotAddDuplicateMatchPlayers_WhenCalled()
    {
        // Arrange
        var homePlayers = Enumerable.Range(0, 5)
            .Select(_ => new TeamPlayer
            {
                PlayerId = PlayerId.New(), 
                TeamId = _sut.HomeTeam!.Value 
                
            }).ToList();

        var awayPlayers = Enumerable.Range(0, 5)
            .Select(_ => new TeamPlayer()
            {
                PlayerId = PlayerId.New(),
                TeamId = _sut.AwayTeam!.Value
            }).ToList();
        
        _sut.AddMatchPlayers([..homePlayers, ..awayPlayers]);

        // Act
        _sut.AddMatchPlayers([..homePlayers, ..awayPlayers]);

        // Assert
        await Verify(_sut, Verify.Settings);
    }
    
    [Fact]
    public void AddMatchPlayersShould_Throw_WhenPlayersAreNotOnAnyMatchTeam()
    {
        // Arrange
        var players = Enumerable.Range(0, 5)
            .Select(_ => new TeamPlayer()
            {
                PlayerId = PlayerId.New(),
                TeamId = TeamId.Empty
            });
        
        // Act
        var act = () => _sut.AddMatchPlayers([..players]);
        
        // Assert   
        act.Should().ThrowExactly<InvalidPlayerTeamException>();
    }

    [Fact]
    public async Task PlanSinglesGamesShould_AddGames_WhenCalled()
    {
        // Arrange
        // Act
        _sut.PlanSinglesGames(2,2);
        
        // Assert
        await Verify(_sut, Verify.Settings);
    }
    
    [Fact]
    public async Task PlanDoublesGamesShould_AddGames_WhenCalled()
    {
        // Arrange
        // Act
        _sut.PlanDoublesGames(2,2);
        
        // Assert
        await Verify(_sut, Verify.Settings);
    }
    
    public static TheoryData<TeamId?, TeamId?> TeamIdConfigurationData()
    {
        return new TheoryData<TeamId?, TeamId?>()
        {
            { null, null },
            { TeamId.New(), null },
            { null, TeamId.New() },
            { TeamId.New(), TeamId.New() },
        };
    }
}