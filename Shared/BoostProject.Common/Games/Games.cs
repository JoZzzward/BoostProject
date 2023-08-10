using BoostProject.Common.Games.Ranks;

namespace BoostProject.Common.Games;

public class Games
{
    public CSGO CSGO = new();
    public DOTA DOTA = new();
}

public enum AllGames
{
    CSGO = 0,
    DOTA = 1
}