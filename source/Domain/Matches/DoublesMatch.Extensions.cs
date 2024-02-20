namespace LeagueBoss.Domain.Matches;

public static class DoublesMatchExtensions
{
    public static void PlanDoublesGames(this DoublesMatch match, int gamesToAdd, int framesPerGame)
    {
        var games = Enumerable.Range(0, gamesToAdd)
            .Select(g => new DoublesGame()
            {
                Match = match,
                HomePairing = match.HomePlayers,
                AwayPairing = match.AwayPlayers,
                GameNumber = g + 1
            })
            .Select(g =>
            {
                g.AddFrames(framesPerGame);
                return g;
            });
        
        match.AddGames([..games]);
    }
}