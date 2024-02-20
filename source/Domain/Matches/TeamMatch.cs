namespace LeagueBoss.Domain.Matches;

using Sports;

public sealed record TeamMatch : Match
{
    private TeamMatch() {}
    
    private readonly List<MatchTeamPlayer> _matchPlayers = [];

    public IReadOnlyCollection<MatchTeamPlayer> MatchPlayers => _matchPlayers.AsReadOnly();

    public TeamId? HomeTeam { get; init; }
    public TeamId? AwayTeam { get; init; }

    internal static TeamMatch Create(PlayerId organiser, SportId sportId, TeamId? homeTeam, TeamId? awayTeam)
    {
        return new TeamMatch { Organiser = organiser, HomeTeam = homeTeam, AwayTeam = awayTeam, SportId = sportId };
    }

    internal void AddMatchPlayers(List<TeamPlayer> teamPlayers)
    {
        if (!TeamPlayersAreHomeOrAwayTeamMembers(teamPlayers))
        {
            throw new InvalidPlayerTeamException(teamPlayers, this);
        }

        var matchPlayers = teamPlayers.NotParticipatingInMatch(this)
            .Select(tp => new MatchTeamPlayer { Match = this, TeamPlayer = tp });

        _matchPlayers.AddRange(matchPlayers);
    }

    private bool TeamPlayersAreHomeOrAwayTeamMembers(IEnumerable<TeamPlayer> teamPlayers)
    {
        return teamPlayers.All(tp => tp.TeamId.Equals(HomeTeam) || tp.TeamId.Equals(AwayTeam));
    }
};