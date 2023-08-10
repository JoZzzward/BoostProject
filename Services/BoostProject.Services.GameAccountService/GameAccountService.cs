using AutoMapper;
using BoostProject.Common.Exceptions;
using BoostProject.Common.Validation;
using BoostProject.Data.Context;
using BoostProject.Data.Entities.GameAccounts;
using BoostProject.Errors;
using BoostProject.Services.GameAccountService.Models;
using BoostProject.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoostProject.Services.GameAccountService;

public class GameAccountService : ServiceManager, IGameAccountService
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    private readonly ILogger<GameAccountService> _logger;
    private readonly IMapper _mapper;
    private readonly IModelValidator<CreateGameAccountModel> _createGameAccountModelValidation;
    private readonly IModelValidator<UpdateGameAccountModel> _updateGameAccountModelValidation;

    public GameAccountService(
        IDbContextFactory<AppDbContext> dbContext,
        ILogger<GameAccountService> logger,
        IMapper mapper,
        IModelValidator<CreateGameAccountModel> createGameAccountModelValidation,
        IModelValidator<UpdateGameAccountModel> updateGameAccountModelValidation)
        : base(dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        _createGameAccountModelValidation = createGameAccountModelValidation;
        _updateGameAccountModelValidation = updateGameAccountModelValidation;
    }

    public async Task<IEnumerable<GameAccountResponse>> GetVerifiedGameAccounts()
    {
        var data = await GetDataFromDatabase(true);
        return data.Select(_mapper.Map<GameAccountResponse>);
    }

    public async Task<IEnumerable<GameAccountResponse>> GetUnverifiedGameAccounts()
    {
        var data = await GetDataFromDatabase(false);
        return data.Select(_mapper.Map<GameAccountResponse>);
    }

    public async Task<GameAccountResponse> GetGameAccountById(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning game account (Id: {GameAccountId})..", id);

        var data = await context
            .GameAccounts
            .FirstOrDefaultAsync(a => a.Id == id);

        ProcessException.ThrowIf(
            () => data == null, 
            LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.NotFound));

        var response = _mapper.Map<GameAccountResponse>(data);

        _logger.LogInformation("Game account  (Id: {GameAccountId}) was successfully returned.", id);

        return response;
    }

    public async Task<VerifiedResult> VerifyGameAccount(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning game account (Id: {GameAccountId})..", id);

        var data = await context
            .GameAccounts
            .FirstOrDefaultAsync(a => a.Id == id);

        ProcessException.ThrowIf(
            () => data == null, 
            LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.NotFound));

        data!.IsVerified = true;

        _logger.LogInformation("Trying to verify game account (Id: {GameAccountId})", id);

        context.GameAccounts.Update(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("Game account successfully verified");

        var response = _mapper.Map<VerifiedResult>(data);

        return response;
    }

    public async Task<CreateGameAccountResponse> CreateGameAccount(CreateGameAccountModel model)
    {
        await ValidateGameAccountModel(model, "on game account creation");

        using var context = await _dbContext.CreateDbContextAsync();

        var data = _mapper.Map<GameAccount>(model);

        _logger.LogInformation("Saving game accounts in database");

        var entry = await context.AddAsync(data);
        await context.SaveChangesAsync();

        _logger.LogInformation("GameAccount was saved successfully");

        var response = _mapper.Map<CreateGameAccountResponse>(entry.Entity);

        return response;
    }

    public async Task<UpdateGameAccountResponse> UpdateGameAccount(Guid id, UpdateGameAccountModel model)
    {
        await ValidateGameAccountModel(model, "on game account updation");

        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning game account (Id: {GameAccountId})..", id);

        var data = await context
            .GameAccounts
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();

        ProcessException.ThrowIf(
            () => data == null, 
            LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.NotFound));

        data = _mapper.Map<GameAccount>(model);

        _logger.LogInformation("Trying to update game account (Id: {GameAccountId})", id);

        context.GameAccounts.Update(data!);
        await context.SaveChangesAsync();

        _logger.LogInformation("Game account successfully updated");

        var response = _mapper.Map<UpdateGameAccountResponse>(data);

        return response;
    }

    public async Task<DeleteGameAccountResponse> DeleteGameAccount(Guid id)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning game account (Id: {GameAccountId})..", id);

        var data = await context
            .GameAccounts
            .FirstOrDefaultAsync(a => a.Id == id);

        ProcessException.ThrowIf(
            () => data == null, 
            LocalizedErrorsManager.GetMessage(ErrorLabels.GameAccount.NotFound));

        _logger.LogInformation("Trying to delete game account (Id: {GameAccountId})", id);

        context.GameAccounts.Remove(data!);
        await context.SaveChangesAsync();

        _logger.LogInformation("Game account successfully deleted");

        var response = _mapper.Map<DeleteGameAccountResponse>(data);

        return response;
    }

    private async Task<List<GameAccount>> GetDataFromDatabase(bool isVerified)
    {
        using var context = await _dbContext.CreateDbContextAsync();

        _logger.LogInformation("Returning game accounts from database..");

        var data = await context
            .GameAccounts
            .Where(x => x.IsVerified == isVerified)
            .ToListAsync() ?? new List<GameAccount>();

        _logger.LogInformation("All game accounts were returned successfully. Amount: {GameAccountsCount}", data.Count);

        return data;
    }

    private async Task<T> ValidateGameAccountModel<T>(T model, string logMessage, string userId = "")
    {
        _logger.LogInformation("Validating model {logMessage}", logMessage);

        var validationTask = model is CreateGameAccountModel createModel ?
            _createGameAccountModelValidation.CheckValidation(createModel) :
            _updateGameAccountModelValidation.CheckValidation(model as UpdateGameAccountModel);

        await validationTask.WaitAsync(new CancellationToken());

        _logger.LogInformation("User verification");

        var id = model is CreateGameAccountModel c ? c.SellerId : Guid.Parse(userId);

        await IsUsersExists(id);

        _logger.LogInformation("Validation is confirmed.");

        return default;
    }
}