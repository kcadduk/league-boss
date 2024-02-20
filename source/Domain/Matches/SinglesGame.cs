namespace LeagueBoss.Domain.Matches;

public record SinglesGame : Game
{
    public PlayerId? HomePlayer { get; init; }
    public PlayerId? AwayPlayer { get; init; }
};