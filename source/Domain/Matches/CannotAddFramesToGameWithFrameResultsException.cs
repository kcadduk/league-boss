namespace LeagueBoss.Domain.Matches;

internal class CannotAddFramesToGameWithFrameResultsException : Exception
{
    public Game Game { get; }

    public CannotAddFramesToGameWithFrameResultsException(Game game)
    {
        Game = game;
    }
}