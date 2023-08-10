using BoostProject.Common.Games;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Data.Entities.Base;

namespace BoostProject.Data.Entities.GameAccounts;

public class GameAccount : BaseEntity
{
    public Guid SellerId { get; set; }
    public virtual AppUser Seller { get; set; }

    public string Label { get; set; }

    public AllGames Game { get; set; }
    public string GameRank { get; set; }

    public bool IsVerified { get; set; }
    public bool IsPrime { get; set; }
    public bool IsSmurf { get; set; }

    public int GameHours { get; set; }
    public int Cost { get; set; }
    public int Discount { get; set; }
}