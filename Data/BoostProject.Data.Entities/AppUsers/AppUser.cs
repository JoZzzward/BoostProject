using BoostProject.Data.Entities.Base;
using BoostProject.Data.Entities.Feedbacks;
using BoostProject.Data.Entities.GameAccounts;
using BoostProject.Data.Entities.Messages;
using BoostProject.Data.Entities.Orders;
using Microsoft.AspNetCore.Identity;

namespace BoostProject.Data.Entities.AppUsers;

public class AppUser : IdentityUser<Guid>, IBaseEntity
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public string ImageUri { get; set; }

    public DateTime CreationDateTime { get; init; }
    public DateTime ModificationDateTime { get; set; }
    public void Touch() => ModificationDateTime = DateTime.Now;

    public virtual ICollection<AppUserRole> Roles { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Feedback> Feedbacks { get; set; }
    public virtual ICollection<GameAccount> GameAccounts { get; set; }
}