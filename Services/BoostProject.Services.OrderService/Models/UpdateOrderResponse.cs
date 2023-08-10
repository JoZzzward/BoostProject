using AutoMapper;
using BoostProject.Data.Entities.Orders;

namespace BoostProject.Services.OrderService.Models;

public class UpdateOrderResponse
{
    public Guid? OrderId { get; set; }
}

public class UpdateOrderResponseProfile : Profile
{
    public UpdateOrderResponseProfile()
    {
        CreateMap<Order, UpdateOrderResponse>()
            .ForMember(dest => dest.OrderId,
                       opt => opt.MapFrom(x => x.Id));
    }
}