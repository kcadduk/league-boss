namespace LeagueBoss.Domain.Matches;

public static class TeamMatchExtensions
{
    public static void PlanSinglesGames(this TeamMatch match, int gamesToAdd, int framesPerGame)
    {
        var games = Enumerable.Range(0, gamesToAdd)
            .Select(g => new SinglesGame()
            {
                Match = match,
                GameNumber = g + 1
            })
            .Select(g =>
            {
                g.AddFrames(framesPerGame);
                return g;
            });
        
        match.AddGames([..games]);
    }

    public static void PlanDoublesGames(this TeamMatch match, int gamesToAdd, int framesPerGame)
    {
        var games = Enumerable.Range(0, gamesToAdd)
            .Select(g => new DoublesGame()
            {
                Match = match,
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