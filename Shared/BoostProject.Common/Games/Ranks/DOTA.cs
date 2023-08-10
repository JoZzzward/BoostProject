namespace BoostProject.Common.Games.Ranks;

public class DOTA
{
    public Dota2Rank Herald { get; set; } = new();
    public Dota2Rank Guardian { get; set; } = new();
    public Dota2Rank Crusader { get; set; } = new();
    public Dota2Rank Archon { get; set; } = new();
    public Dota2Rank Legend { get; set; } = new();
    public Dota2Rank Ancient { get; set; } = new();
    public Dota2Rank Divine { get; set; } = new();
    public Dota2Rank Immortal { get; set; } = new();
}

public class Dota2Rank
{
    public int MatchmakingPoints
    {
        get { return MatchmakingPoints; }
        set => MatchmakingPoints = value > 6000 ? 6000 : value;
    }
}