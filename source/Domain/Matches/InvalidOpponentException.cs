namespace LeagueBoss.Domain.Matches;

internal class InvalidOpponentException : Exception
{
    public PlayerPairing? HomePlayers { get; }
    public PlayerPairing? AwayPlayers { get; }
    public TeamPlayer? PlayerOne { get; }
    public TeamPlayer? PlayerTwo { get; }

    public InvalidOpponentException(TeamPlayer? playerOne, TeamPlayer? playerTwo)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
    }

    public InvalidOpponentException(PlayerPairing? homePlayers, PlayerPairing? awayPlayers)
    {
        HomePlayers = homePlayers;
        AwayPlayers = awayPlayers;
    }
}