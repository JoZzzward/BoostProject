using AutoMapper;
using BoostProject.Api.Controllers.GameAccounts.Models;
using BoostProject.Common.Consts;
using BoostProject.Common.Enums;
using BoostProject.Common.Security;
using BoostProject.Services.GameAccountService;
using BoostProject.Services.GameAccountService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BoostProject.Api.Controllers.GameAccounts;

[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class GameAccountsController : ControllerBase
{
    private readonly IGameAccountService _gameAccountService;
    private readonly IMapper _mapper;
    private readonly ILogger<GameAccountsController> _logger;

    public GameAccountsController(
        IGameAccountService gameAccountService,
        IMapper mapper,
        ILogger<GameAccountsController> logger
        )
    {
        _gameAccountService = gameAccountService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("verified")]
    public async Task<IEnumerable<GameAccountResponse>> GetVerifiedGameAccounts()
    {
        _logger.LogInformation("Returning verified game accounts..");

        return await _gameAccountService.GetVerifiedGameAccounts();
    }

    [HttpGet("unverified")]
    [Authorize]
    public async Task<IEnumerable<GameAccountResponse>> GetUnverifiedGameAccounts()
    {
        _logger.LogInformation("Returning unverified game accounts..");

        return await _gameAccountService.GetUnverifiedGameAccounts();
    }

    [HttpGet("{id}")]
    public async Task<GameAccountResponse> GetGameAccountById([FromRoute] Guid id)
    {
        _logger.LogInformation("Returning game accounts by sell..");

        return await _gameAccountService.GetGameAccountById(id);
    }

    [HttpPost("verify/{id}")]
    [Authorize(Policy = AppScopes.VerifyAccount, Roles = nameof(UserPermissions.Admin))]
    public async Task<VerifiedResult> VerifyGameAccount([FromRoute] Guid id)
    {
        _logger.LogInformation("Game account confirmation is in progress..");

        return await _gameAccountService.VerifyGameAccount(id);
    }

    [HttpPost]
    [Authorize/*(Policy = AppScopes.GameAccountsWrite)*/]
    public async Task<CreateGameAccountResponse> CreateGameAccount([FromBody] CreateGameAccountRequest request)
    {
        _logger.LogInformation("Creating game account..");

        var model = _mapper.Map<CreateGameAccountModel>(request);

        return await _gameAccountService.CreateGameAccount(model);
    }

    [HttpPut("{id}")]
    [Authorize/*(Policy = AppScopes.GameAccountsWrite)*/]
    public async Task<UpdateGameAccountResponse> UpdateGameAccount([FromRoute] Guid id,[FromBody] UpdateGameAccountRequest request)
    {
        _logger.LogInformation("Updating game account..");

        var model = _mapper.Map<UpdateGameAccountModel>(request);

        return await _gameAccountService.UpdateGameAccount(id, model);
    }

    [HttpDelete("{id}")]
    [Authorize/*(Policy = AppScopes.GameAccountsWrite)*/]
    public async Task<DeleteGameAccountResponse> DeleteGameAccount([FromRoute] Guid id)
    {
        _logger.LogInformation("Removing game account from database..");

        return await _gameAccountService.DeleteGameAccount(id);
    }
}
