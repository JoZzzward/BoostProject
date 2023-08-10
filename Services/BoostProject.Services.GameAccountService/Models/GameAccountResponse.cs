using AutoMapper;
using BoostProject.Common.Games;
using BoostProject.Data.Entities.GameAccounts;

namespace BoostProject.Services.GameAccountService.Models;

public class GameAccountResponse
{
    public Guid SellerId { get; set; }

    public string Label { get; set; }

    public AllGames Game { get; set; }
    public string? GameRank { get; set; }

    public bool IsPrime { get; set; }
    public bool IsSmurf { get; set; }

    public int GameHours { get; set; }
    public int Cost { get; set; }
    public int Discount { get; set; }
}

public class GameAccountResponseProfile : Profile
{
    public GameAccountResponseProfile()
    {
        CreateMap<GameAccount, GameAccountResponse>();
    }
}
