using Microsoft.AspNetCore.Identity;

namespace BoostProject.Data.Entities.AppUsers;

public class AppUserRole : IdentityUserRole<Guid>
{
    public Guid UserId { get; set; }
    public virtual AppUser User { get; set; }

    public Guid RoleId { get; set; }
    public virtual AppRole Role { get; set; }
}