using BoostProject.Data.Entities.AppUsers;
using BoostProject.Data.Entities.Base;

namespace BoostProject.Data.Entities.Feedbacks;

public class Feedback : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual AppUser User { get; set; }

    public string Content { get; set; }
}