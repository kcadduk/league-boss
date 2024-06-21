namespace LeagueBoss.Domain.Tests.Unit.Matches;

using Domain.Matches;
using FluentAssertions;
using Sports;

public class MatchTests
{
    [Fact]
    public async Task AddGamesShould_AddMatchGames_WhenGamesAreForMatch()
    {
        // Arrange
        var sut = new MatchStub()
        {
            Organiser = PlayerId.Empty,
            SportId = SportId.Empty
        };
        
        var games = Enumerable.Range(0, 5)
            .Select(_ => new GameStub()
            {
                Match = sut,
            });
        
        // Act
        sut.AddGames([..games]);
        
        // Assert
        await Verify(sut, Verify.Settings);
    }       
    
    [Fact]
    public void AddGamesShould_Throw_WhenGamesAreForMatch()
    {
        // Arrange
        var sut = new MatchStub()
        {
            Organiser = PlayerId.Empty,
            SportId = SportId.Empty
        };
        
        var otherMatch = new MatchStub()
        {
            Organiser = PlayerId.Empty,
            SportId = SportId.Empty
        };
        
        var games = Enumerable.Range(0, 5)
            .Select(_ => new GameStub()
            {
                Match = otherMatch,
            });
        
        // Act
        var act = () => sut.AddGames([..games]);
        
        
        // Assert
        act.Should().ThrowExactly<InvalidGamesForMatchException>();
    }   
  
    [Fact]
    public async Task AddGamesShould_NotAddDuplicateGames_WhenCalled()
    {
        // Arrange
        var sut = new MatchStub()
        {
            Organiser = PlayerId.Empty,
            SportId = SportId.Empty
        };
        
        var games = Enumerable.Range(0, 5)
            .Select(_ => new GameStub
            {
                Match = sut,
            }).ToList();
        
        sut.AddGames([..games]);
        
        // Act
        sut.AddGames([..games]);
        
        
        // Assert
        await Verify(sut, Verify.Settings);
    }   
  
    
    
    public record MatchStub : Match
    {
    }

    public record GameStub : Game
    {
    }
}