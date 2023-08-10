using AutoMapper;
using BoostProject.Data.Entities.Orders;

namespace BoostProject.Services.OrderService.Models;

public class CreateOrderResponse
{
    public Guid? OrderId { get; set; }
}

public class CreateOrderResponseProfile : Profile
{
    public CreateOrderResponseProfile()
    {
        CreateMap<Order, CreateOrderResponse>()
            .ForMember(dest => dest.OrderId,
                       opt => opt.MapFrom(x => x.Id));
    }
}