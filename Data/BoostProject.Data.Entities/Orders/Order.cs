using BoostProject.Common.Enums;
using BoostProject.Common.Games;
using BoostProject.Data.Entities.AppUsers;
using BoostProject.Data.Entities.Base;

namespace BoostProject.Data.Entities.Orders;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public virtual AppUser Customer { get; set; }

    public Guid? BoosterId { get; set; }
    public virtual AppUser? Booster { get; set; }

    public AllGames Game { get; set; }

    public int Cost { get; set; }

    public OrderStatus Status { get; set; }
}