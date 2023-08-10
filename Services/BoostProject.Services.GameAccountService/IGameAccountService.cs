using BoostProject.Services.GameAccountService.Models;

namespace BoostProject.Services.GameAccountService;

public interface IGameAccountService
{
    Task<IEnumerable<GameAccountResponse>> GetVerifiedGameAccounts();
    Task<IEnumerable<GameAccountResponse>> GetUnverifiedGameAccounts();
    Task<GameAccountResponse> GetGameAccountById(Guid id);
    Task<CreateGameAccountResponse> CreateGameAccount(CreateGameAccountModel model);
    Task<UpdateGameAccountResponse> UpdateGameAccount(Guid id, UpdateGameAccountModel model);
    Task<VerifiedResult> VerifyGameAccount(Guid id);
    Task<DeleteGameAccountResponse> DeleteGameAccount(Guid id);
}