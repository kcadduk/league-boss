namespace LeagueBoss.Domain.Matches;

public class InvalidGamesForMatchException : Exception
{
    public List<Game> Games { get; }
    public Match Match { get; }

    public InvalidGamesForMatchException(List<Game> games, Match match)
    {
        Games = games;
        Match = match;
    }
}