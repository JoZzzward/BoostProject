using AutoMapper;
using BoostProject.Data.Entities.Orders;

namespace BoostProject.Services.OrderService.Models;

public class OrderResponse
{
    public Guid OrderId { get; set; }
}

public class OrderResponseProfile : Profile
{
    public OrderResponseProfile()
    {
        CreateMap<Order,  OrderResponse>()
            .ForMember(dest => dest.OrderId,
                       opt => opt.MapFrom(x => x.Id));
    }
}