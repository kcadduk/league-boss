namespace LeagueBoss.Domain.Tests.Unit.Matches;

using Domain.Matches;
using FluentAssertions;
using Sports;

public class SinglesMatchTests
{
    [Theory]
    [MemberData(nameof(HomeAndAwayPlayerConfigurationData))]
    public async Task CreateShould_CreateAMatch_WhenCalled(PlayerId? homePlayer, PlayerId? awayPlayer)
    {
        // Arrange

        // Act
        var res = SinglesMatch.Create(PlayerId.New(), homePlayer, awayPlayer, SportId.New());

        // Assert
        var settings = Verify.Settings;
        settings.UseHashedParameters(homePlayer.HasValue, awayPlayer.HasValue);
        await Verify(res, settings);
    }

    public static TheoryData<PlayerId?, PlayerId?> HomeAndAwayPlayerConfigurationData()
    {
        return new TheoryData<PlayerId?, PlayerId?>()
        {
            { PlayerId.New(), PlayerId.New() },
            { null, PlayerId.New() },
            { PlayerId.New(), null },
            { null, null }
        };
    }

    [Fact]
    public void CreateShould_Throw_WhenPlayersAreNotNullAndSamePerson()
    {
        // Arrange

        // Act
        var act = () => SinglesMatch.Create(PlayerId.New(), PlayerId.Empty, PlayerId.Empty, SportId.New());

        // Assert
        act.Should().ThrowExactly<CannotPlayYourselfException>();
    }

    [Theory]
    [MemberData(nameof(HomeAndAwayPlayerConfigurationData))]
    public async Task PlanSinglesMatchShould_AddRequiredGamesAndFrames_WhenCalled(PlayerId? homePlayer, PlayerId? awayPlayer)
    {
        // Arrange
        var sut = SinglesMatch.Create(PlayerId.New(), homePlayer, awayPlayer, SportId.New());
        
        // Act
        sut.PlanSinglesMatch(2, 2);

        // Assert
        var verifySettings = Verify.Settings;
        verifySettings.UseHashedParameters(homePlayer.HasValue, awayPlayer.HasValue);
        await Verify(sut, verifySettings);
    }
}