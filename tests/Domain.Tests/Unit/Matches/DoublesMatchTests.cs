namespace LeagueBoss.Domain.Tests.Unit.Matches;

using Domain.Matches;
using FluentAssertions;
using Sports;

public class DoublesMatchTests
{
    private readonly DoublesMatch _sut = DoublesMatch.Create(PlayerId.New(), SportId.New());
    [Fact]
    public async Task CreateShould_CreateADoublesMatch_WhenCalled()
    {
        // Arrange

        // Act
        var res = DoublesMatch.Create(PlayerId.New(), SportId.New());
        
        // Assert
        await Verify(res, Verify.Settings);
    }

    [Fact]
    public async Task AddPlayerPairingsShould_AddPlayerPairings_WhenCalled()
    {
        // Arrange
        var playerPairingOne = new PlayerPairing()
        {
            PlayerOne = PlayerId.New(),
            PlayerTwo = PlayerId.New()
        };    
        var playerPairingTwo = new PlayerPairing()
        {
            PlayerOne = PlayerId.New(),
            PlayerTwo = PlayerId.New()
        };
        // Act
        _sut.AddPlayerPairings(playerPairingOne, playerPairingTwo);
        
        // Assert
        await Verify(_sut, Verify.Settings);
    }

    [Theory]
    [MemberData(nameof(AddPlayerPairingsShould_Throw_WhenPlayerPairingsAreSameOrIntersected_Data))]
    public void AddPlayerPairingsShould_Throw_WhenPlayerPairingsAreSameOrIntersected(PlayerPairing firstPairing, PlayerPairing secondPairing)
    {
        // Arrange

        // Act
        var act = () => _sut.AddPlayerPairings(firstPairing, secondPairing);
        
        // Assert
        act.Should().ThrowExactly<InvalidOpponentException>();
    }

    public static TheoryData<PlayerPairing, PlayerPairing> 
        AddPlayerPairingsShould_Throw_WhenPlayerPairingsAreSameOrIntersected_Data()
    {
        return new TheoryData<PlayerPairing, PlayerPairing>()
        {
            {
                new PlayerPairing()
                {
                    PlayerOne = PlayerId.Empty,
                    PlayerTwo = PlayerId.New()
                },
                new PlayerPairing()
                {
                    PlayerOne = PlayerId.Empty,
                    PlayerTwo = PlayerId.New()
                }
            },
            {
                new PlayerPairing()
                {
                    PlayerOne = PlayerId.New(),
                    PlayerTwo = PlayerId.Empty
                },
                new PlayerPairing()
                {
                    PlayerOne = PlayerId.New(),
                    PlayerTwo = PlayerId.Empty
                }
            },
            {
                new PlayerPairing()
                {
                    PlayerOne = PlayerId.Empty,
                    PlayerTwo = PlayerId.Empty
                },
                new PlayerPairing()
                {
                    PlayerOne = PlayerId.Empty,
                    PlayerTwo = PlayerId.Empty
                }
            },
        };
    }
    
    [Fact]
    public async Task PlanDoublesGamesShould_PlanDoublesGames_WhenCalled()
    {
        // Arrange

        // Act
        _sut.PlanDoublesGames(2, 2);
        
        // Assert
        await Verify(_sut, Verify.Settings);
    }
}