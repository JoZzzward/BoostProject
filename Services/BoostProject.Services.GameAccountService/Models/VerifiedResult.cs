using AutoMapper;
using BoostProject.Data.Entities.GameAccounts;

namespace BoostProject.Services.GameAccountService.Models;

public class VerifiedResult
{
    public Guid GameAccountId { get; set; }
    public Guid SellerId { get; set; }
    public bool IsVerified { get; set; }
}

public class VerifiedResultProfile : Profile
{
    public VerifiedResultProfile()
    {
        CreateMap<GameAccount, VerifiedResult>()
            .ForMember(dest => dest.GameAccountId, 
            opt => opt.MapFrom(x => x.Id))
            .ForMember(dest => dest.SellerId, 
            opt => opt.MapFrom(x => x.SellerId))
            .ForMember(dest => dest.IsVerified, 
            opt => opt.MapFrom(x => x.IsVerified));
    }
}