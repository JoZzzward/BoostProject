using AutoMapper;
using BoostProject.Data.Entities.GameAccounts;

namespace BoostProject.Services.GameAccountService.Models;

public class CreateGameAccountResponse
{
    public Guid GameAccountId { get; set; }
}

public class CreateGameAccountResponseProfile : Profile
{
    public CreateGameAccountResponseProfile()
    {
        CreateMap<GameAccount, CreateGameAccountResponse>()
            .ForMember(dest => dest.GameAccountId, opt => opt.MapFrom(x => x.Id));
    }
}