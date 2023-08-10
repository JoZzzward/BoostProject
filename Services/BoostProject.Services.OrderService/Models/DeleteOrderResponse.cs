using AutoMapper;
using BoostProject.Data.Entities.Orders;

namespace BoostProject.Services.OrderService.Models;

public class DeleteOrderResponse
{
    public Guid? OrderId { get; set; }
}

public class DeleteOrderResponseProfile : Profile
{
    public DeleteOrderResponseProfile()
    {
        CreateMap<Order, DeleteOrderResponse>()
            .ForMember(dest => dest.OrderId,
                       opt => opt.MapFrom(x => x.Id));
    }
}