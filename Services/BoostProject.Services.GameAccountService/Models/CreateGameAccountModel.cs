using AutoMapper;
using BoostProject.Common.Games;
using BoostProject.Data.Entities.GameAccounts;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.GameAccountService.Models;

public class CreateGameAccountModel
{
    public Guid SellerId { get; set; }

    public string Label { get; set; }

    public AllGames Game { get; set; }
    public string GameRank { get; set; }

    public bool IsPrime { get; set; }
    public bool IsSmurf { get; set; }

    public int GameHours { get; set; }
    public int Cost { get; set; }
    public int Discount { get; set; }
}

public class CreateGameAccountModelProfile : Profile
{
    public CreateGameAccountModelProfile()
    {
        CreateMap<CreateGameAccountModel, GameAccount>();
    }
}

public class CreateGameAccountModelValidation : AbstractValidator<CreateGameAccountModel>
{
    public CreateGameAccountModelValidation()
    {
        RuleFor(x => x.Label)
            .MaximumLength(100)
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.LabelMaxLength));
        RuleFor(x => x.GameHours)
            .NotNull()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.GameHoursMustBeNotEmpty));
        RuleFor(x => x.Cost)
            .NotEmpty()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.CostMustBeNotEmpty));
    }
}
