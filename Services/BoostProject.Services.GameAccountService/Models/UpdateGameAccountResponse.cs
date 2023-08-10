using AutoMapper;
using BoostProject.Data.Entities.GameAccounts;

namespace BoostProject.Services.GameAccountService.Models;

public class UpdateGameAccountResponse
{
    public Guid GameAccountId { get; set; }
}

public class UpdateGameAccountResponseProfile : Profile
{
    public UpdateGameAccountResponseProfile()
    {
        CreateMap<GameAccount, UpdateGameAccountResponse>()
            .ForMember(dest => dest.GameAccountId, opt => opt.MapFrom(x => x.Id));
    }
}