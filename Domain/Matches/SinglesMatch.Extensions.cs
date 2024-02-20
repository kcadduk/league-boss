namespace LeagueBoss.Domain.Matches;

public static class SinglesMatchExtensions
{
    public static void PlanSinglesMatch(this SinglesMatch match, int gamesPerMatch, int framesPerGame)
    {
        var games = Enumerable.Range(0, gamesPerMatch)
            .Select(g => new SinglesGame
            {
                Match = match,
                GameNumber = g + 1,
                HomePlayer = match.HomePlayer,
                AwayPlayer = match.AwayPlayer
            })
            .Select(g =>
            {
                g.AddFrames(framesPerGame);
                return g;
            });
        
        match.AddGames([..games]);
    }
}