using AutoMapper;
using BoostProject.Common.Enums;
using BoostProject.Common.Games;
using BoostProject.Data.Entities.Orders;
using BoostProject.Errors;
using FluentValidation;

namespace BoostProject.Services.OrderService.Models;

public class UpdateOrderModel
{
    public Games Game { get; set; }
    public int Cost { get; set; }
    public OrderStatus Status { get; set; }
}

public class UpdateOrderModelProfile : Profile
{
    public UpdateOrderModelProfile()
    {
        CreateMap<UpdateOrderModel, Order>();
    }
}

public class UpdateOrderModelValidation : AbstractValidator<UpdateOrderModel>
{
    public UpdateOrderModelValidation()
    {
        RuleFor(x => x.Game)
           .NotNull()
           .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Order.GameIsRequired));

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage(LocalizedErrorsManager.GetMessage(ErrorLabels.Order.IncorrectStatus));
    }
}