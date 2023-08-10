namespace BoostProject.Api.Controllers.Feedbacks.Models;

public class CreateFeedbackRequest
{
    public Guid UserId { get; set; }
    public string Content { get; set; }
}
