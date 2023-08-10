using AutoMapper;
using BoostProject.Data.Entities.Feedbacks;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.FeedbackService.Models;

public class CreateFeedbackModel
{
    public Guid UserId { get; set; }
    public string? Content { get; set; }
}

public class CreateFeedbackModelProfile : Profile
{
    public CreateFeedbackModelProfile()
    {
        CreateMap<CreateFeedbackModel, Feedback>();
    }
}

public class CreateFeedbackModelValidation : AbstractValidator<CreateFeedbackModel>
{
    public CreateFeedbackModelValidation()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Feedback.FeedbackContentIsRequired))
            .NotNull()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Feedback.FeedbackContentIsRequired))
            .MaximumLength(250)
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Feedback.FeedbackContentMustBeLessThan250Symbols));
    }
}