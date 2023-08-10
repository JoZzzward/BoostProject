using BoostProject.Data.Entities.AppUsers;
using BoostProject.Data.Entities.Base;
using Data.Entities.Messages;

namespace BoostProject.Data.Entities.Messages;

public class Message : BaseEntity
{
    public Guid SenderId { get; set; }
    public virtual AppUser Sender { get; set; }

    public Guid ReceiverId { get; set; }
    public virtual AppUser Receiver { get; set; }

    public string Content { get; set; }

    public SeenStatus Seen { get; set; }
}
