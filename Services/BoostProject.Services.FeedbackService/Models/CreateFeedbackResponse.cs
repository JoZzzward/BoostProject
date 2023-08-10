using AutoMapper;
using BoostProject.Data.Entities.Feedbacks;

namespace BoostProject.Services.FeedbackService.Models;

public class CreateFeedbackResponse
{
    public Guid FeedbackId { get; set; }
}
public class CreateFeedbackResponseProfile : Profile
{

    public CreateFeedbackResponseProfile()
    {
        CreateMap<Feedback, CreateFeedbackResponse>()
            .ForMember(dest => dest.FeedbackId, 
                      opt => opt.MapFrom(x => x.Id));
    }

}