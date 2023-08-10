using AutoMapper;
using BoostProject.Api.Controllers.Feedbacks;
using BoostProject.Api.Controllers.Orders.Models;
using BoostProject.Common.Consts;
using BoostProject.Services.OrderService.Models;
using BoostProject.Services.OrdersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoostProject.Api.Controllers.Orders;

[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[EnableCors(PolicyName = CorsConsts.DefaultOriginName)]
[Authorize]
[ApiVersion("1.0")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;
    private readonly IMapper _mapper;
    private readonly ILogger<FeedbacksController> _logger;

    public OrdersController(
        IOrdersService ordersService,
        IMapper mapper,
        ILogger<FeedbacksController> logger
        )
    {
        _ordersService = ordersService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderResponse>> GetAllOrders()
    {
        _logger.LogInformation("Returning all orders..");

        return await _ordersService.GetAllOrders();
    }

    [HttpGet("unassigned-orders")]
    public async Task<IEnumerable<OrderResponse>> GetUnassignedOrders()
    {
        _logger.LogInformation("Returning all unassigned orders..");

        return await _ordersService.GetUnassignedOrders();
    }

    [HttpGet("{id}")]
    public async Task<OrderResponse> GetOrderById([FromRoute] Guid id)
    {
        _logger.LogInformation("Returning all orders..");

        return await _ordersService.GetOrderById(id);
    }

    [HttpPost]
    public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
    {
        _logger.LogInformation("Creating order..");

        var model = _mapper.Map<CreateOrderModel>(request);

        return await _ordersService.CreateOrder(model);
    }

    [HttpPut("{id}")]
    public async Task<UpdateOrderResponse> UpdateOrder([FromRoute] Guid id, UpdateOrderRequest request)
    {
        _logger.LogInformation("Updating order model (Id: {OrderId})", id);

        var model = _mapper.Map<UpdateOrderModel>(request);

        return await _ordersService.UpdateOrder(id, model);
    }

    [HttpDelete("{id}")]
    public async Task<DeleteOrderResponse> DeleteOrder([FromRoute] Guid id)
    {
        _logger.LogInformation("Removing order (Id: {OrderId})", id);

        return await _ordersService.DeleteOrder(id);
    }

    [HttpPost("{orderId}")]
    public async Task<OrderResponse> AssignOrderToBooster([FromRoute] Guid orderId, [FromQuery] Guid boosterId)
    {
        _logger.LogInformation("Assigning order (Id: {OrderId}) to booster (Id: {BoosterId})", orderId, boosterId);

        return await _ordersService.AssignOrderToBooster(orderId, boosterId);
    }
}
