using AutoMapper;
using BoostProject.Common.Enums;
using BoostProject.Common.Games;
using BoostProject.Data.Entities.Orders;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.OrderService.Models;

public class CreateOrderModel
{
    public Guid CustomerId { get; set; }
    public Games Game { get; set; }
    public int Cost { get; set; }
    public OrderStatus Status { get; set; }
}

public class CreateOrderModelProfile : Profile
{
    public CreateOrderModelProfile()
    {
        CreateMap<CreateOrderModel, Order>();
    }
}

public class CreateOrderModelValidation : AbstractValidator<CreateOrderModel>
{
    public CreateOrderModelValidation()
    {
        RuleFor(x => x.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.IncorrectParameter));

        RuleFor(x => x.Game)
            .NotNull()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.IncorrectParameter));

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.IncorrectParameter));
    }
}