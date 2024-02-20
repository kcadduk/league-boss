namespace LeagueBoss.Domain.Matches;

public record MatchTeamPlayer
{
    internal MatchTeamPlayer()
    {
    }
    public required Match Match { get; init; }
   
    public required TeamPlayer TeamPlayer { get; init; }
}