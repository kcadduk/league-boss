namespace LeagueBoss.Domain.Matches;

using Sports;

public sealed record DoublesMatch : Match
{
    private DoublesMatch()
    {
    }

    
    public PlayerPairing? HomePlayers { get; private set; }
    public PlayerPairing? AwayPlayers { get; private set; }

    public static DoublesMatch Create(PlayerId organizer, SportId sportId)
    {
        return new DoublesMatch
        {
            SportId = sportId,
            Organiser = organizer
        };
    }
    
    public void AddPlayerPairings(PlayerPairing homePlayers, PlayerPairing awayPlayers)
    {
        if (homePlayers.Equals(awayPlayers) || homePlayers.HasIntersectedPlayers(awayPlayers))
        {
            throw new InvalidOpponentException(homePlayers, awayPlayers);
        }
        
        HomePlayers = homePlayers;
        AwayPlayers = awayPlayers;
    }
};