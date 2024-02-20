namespace LeagueBoss.Domain.Matches;

internal class InvalidPlayerTeamException : Exception
{
    public List<TeamPlayer> TeamPlayers { get; } = new();
    public Match Match { get; }

    public InvalidPlayerTeamException(List<TeamPlayer> teamPlayers, Match match)
    {
        TeamPlayers = teamPlayers;
        Match = match;
    }

    public InvalidPlayerTeamException(TeamPlayer? playerOne, TeamPlayer? playerTwo, Match match)
    {
        Match = match;
        
        if(playerOne is not null)
        {
            TeamPlayers.Add(playerOne);
        }

        if (playerTwo is not null)
        {
            TeamPlayers.Add(playerTwo);
        }
    }
}