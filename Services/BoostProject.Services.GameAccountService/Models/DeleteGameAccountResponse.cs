using AutoMapper;
using BoostProject.Data.Entities.GameAccounts;

namespace BoostProject.Services.GameAccountService.Models;

public class DeleteGameAccountResponse
{
    public Guid GameAccountId { get; set; }
}

public class DeleteGameAccountResponseProfile : Profile
{
    public DeleteGameAccountResponseProfile()
    {
        CreateMap<GameAccount, DeleteGameAccountResponse>()
            .ForMember(dest => dest.GameAccountId, opt => opt.MapFrom(x => x.Id));
    }
}