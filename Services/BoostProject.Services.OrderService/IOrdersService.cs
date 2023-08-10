using BoostProject.Services.OrderService.Models;

namespace BoostProject.Services.OrdersService;

public interface IOrdersService
{
    Task<IEnumerable<OrderResponse>> GetAllOrders();
    Task<IEnumerable<OrderResponse>> GetUnassignedOrders();
    Task<OrderResponse> GetOrderById(Guid id);
    Task<CreateOrderResponse> CreateOrder(CreateOrderModel model);
    Task<UpdateOrderResponse> UpdateOrder(Guid id, UpdateOrderModel model);
    Task<DeleteOrderResponse> DeleteOrder(Guid id);
    Task<OrderResponse> AssignOrderToBooster(Guid orderId, Guid boosterId);
}