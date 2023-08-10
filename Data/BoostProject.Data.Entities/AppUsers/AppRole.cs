using BoostProject.Common.Enums;
using BoostProject.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace BoostProject.Data.Entities.AppUsers;

public class AppRole : IdentityRole<Guid>, IBaseEntity
{
    public UserPermissions Permission { get; set; }

    public DateTime CreationDateTime { get; init; } = DateTime.Now;

    public DateTime ModificationDateTime { get; set; }

    public void Touch() => ModificationDateTime = DateTime.Now;

    public virtual ICollection<AppUserRole> Users { get; set; }

    public AppRole() { }
    public AppRole(string role) 
        :base(role)
    {
        Permission = (UserPermissions)Enum.Parse(typeof(UserPermissions), role);
    }
}