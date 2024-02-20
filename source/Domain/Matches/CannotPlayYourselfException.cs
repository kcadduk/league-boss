namespace LeagueBoss.Domain.Matches;

public class CannotPlayYourselfException : Exception
{
    public PlayerId? HomePlayer { get; }
    public PlayerId? AwayPlayer { get; }

    public CannotPlayYourselfException(PlayerId? homePlayer, PlayerId? awayPlayer)
    {
        HomePlayer = homePlayer;
        AwayPlayer = awayPlayer;
    }
}