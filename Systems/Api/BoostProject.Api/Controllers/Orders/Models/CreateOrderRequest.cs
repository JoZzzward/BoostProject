using AutoMapper;
using BoostProject.Common.Enums;
using BoostProject.Common.Games;
using BoostProject.Services.OrderService.Models;

namespace BoostProject.Api.Controllers.Orders.Models;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public Games Game { get; set; }
    public int Cost { get; set; }
    public OrderStatus Status { get; set; }
}

public class CreateOrderRequestProfile : Profile
{
    public CreateOrderRequestProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderModel>();
    }
}