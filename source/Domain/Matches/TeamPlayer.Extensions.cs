namespace LeagueBoss.Domain.Matches;

public static class TeamPlayerExtensions
{
    public static IEnumerable<TeamPlayer> NotParticipatingInMatch(this IEnumerable<TeamPlayer> players, TeamMatch match)
    {
        return players.Where(tp => match.MatchPlayers.All(mp => mp.TeamPlayer.PlayerId != tp.PlayerId));
    }
}