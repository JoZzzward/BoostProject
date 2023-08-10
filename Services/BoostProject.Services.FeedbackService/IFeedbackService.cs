using BoostProject.Services.FeedbackService.Models;

namespace BoostProject.Services.FeedbackService;

public interface IFeedbackService
{
    Task<IEnumerable<FeedbackResponse>> GetAllFeedbacks();
    Task<CreateFeedbackResponse> CreateFeedback(CreateFeedbackModel model);
}