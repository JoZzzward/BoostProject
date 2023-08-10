namespace BoostProject.Services.MessagesService.Models;

public class DeleteMessageModel
{
    public Guid SenderId { get; set; }
    public Guid MessageId { get; set; }
}