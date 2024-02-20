namespace LeagueBoss.Domain.Matches;

public record DoublesGame : Game
{
    public PlayerPairing? HomePairing { get; init; }
    public PlayerPairing? AwayPairing { get; init; }
}