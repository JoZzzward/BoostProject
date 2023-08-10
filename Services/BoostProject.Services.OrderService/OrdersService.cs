using AutoMapper;
using BoostProject.Common.Exceptions;
using BoostProject.Common.Validation;
using BoostProject.Data.Context;
using BoostProject.Data.Entities.Orders;
using BoostProject.Errors;
using BoostProject.Services.OrderService.Models;
using BoostProject.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoostProject.Services.OrdersService;

public class OrdersService : ServiceManager, IOrdersService
{

    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<OrdersService> _logger;
    private readonly IMapper _mapper;
    private readonly IModelValidator<CreateOrderModel> _createOrderModelValidation;
    private readonly IModelValidator<UpdateOrderModel> _updateOrderModelValidation;

    public OrdersService(
        IDbContextFactory<AppDbContext> dbContext,
        ILogger<OrdersService> logger,
        IMapper mapper,
        IModelValidator<CreateOrderModel> createOrderModelValidation,
        IModelValidator<UpdateOrderModel> updateOrderModelValidation)
        : base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _createOrderModelValidation = createOrderModelValidation;
        _updateOrderModelValidation = updateOrderModelValidation;
    }

    public async Task<IEnumerable<OrderResponse>> GetAllOrders()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning orders  from database..");

        var data = await context
            .Orders
            .ToListAsync() ?? default;

        var response = data.Select(_mapper.Map<OrderResponse>);

        _logger.LogInformation("All orders was returned successfully. Amount: {OrdersCount}", data.Count);

        return response;
    }

    public async Task<IEnumerable<OrderResponse>> GetUnassignedOrders()
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning orders  from database..");

        var data = await context
            .Orders
            .Where(x => x.BoosterId == null || x.BoosterId == Guid.Empty)
            .ToListAsync() ?? default;

        _logger.LogInformation("All unassigned orders was returned successfully. Amount: {OrdersCount}", data.Count);

        return data.Select(_mapper.Map<OrderResponse>);
    }

    public async Task<OrderResponse> GetOrderById(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning order (Id: {OrderId}) from database..", id);

        var data = await context
            .Orders
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync() ?? default;

        _logger.LogInformation("Order (Id: {OrderId}) was returned successfully.", id);

        return _mapper.Map<OrderResponse>(data);
    }

    public async Task<CreateOrderResponse> CreateOrder(CreateOrderModel model)
    {
        await _createOrderModelValidation.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var data = _mapper.Map<Order>(model);

        _logger.LogInformation("Saving order in database..");

        var entry = await context.AddAsync(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("Order was saved successfully");

        return _mapper.Map<CreateOrderResponse>(entry.Entity);
    }

    public async Task<UpdateOrderResponse> UpdateOrder(Guid id, UpdateOrderModel model)
    {
        await _updateOrderModelValidation.CheckValidation(model);

        using var context = await _dbContext.CreateDbContextAsync();

        var data = await context.Orders
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        ProcessException.ThrowIf(() => data == null, LocalizedErrorsManager.GetMessage(ErrorLabels.ElementNotFound));

        data = _mapper.Map<Order>(model);

        _logger.LogInformation("Updating order in database..");

        var entry = context.Update(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("Order (Id: {OrderId}) was updated successfully", id);

        return _mapper.Map<UpdateOrderResponse>(entry.Entity);
    }

    public async Task<DeleteOrderResponse> DeleteOrder(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var data = await context.Orders
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        ProcessException.ThrowIf(() => data == null, LocalizedErrorsManager.GetMessage(ErrorLabels.ElementNotFound));

        _logger.LogInformation("Removing order in database..");

        var entry = context.Remove(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("Order (Id: {OrderId}) was removed successfully", id);

        return _mapper.Map<DeleteOrderResponse>(entry.Entity);
    }

    public async Task<OrderResponse> AssignOrderToBooster(Guid orderId, Guid boosterId)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        var data = await context.Orders
            .Where(x => x.Id == orderId && (x.BoosterId == null || x.BoosterId == Guid.Empty))
            .FirstOrDefaultAsync();

        ProcessException.ThrowIf(() => data == null, LocalizedErrorsManager.GetMessage(ErrorLabels.ElementNotFound));

        data!.BoosterId = boosterId;

        _logger.LogInformation("Assigning order (Id: {OrderId}) to booster (Id: {BoosterId})..", orderId, boosterId);

        var entry = context.Update(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("Order (Id: {OrderId}) was successfully assigned to booster (Id: {BoosterId})", orderId, boosterId);

        return _mapper.Map<OrderResponse>(entry.Entity);
    }
}