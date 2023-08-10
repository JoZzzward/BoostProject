using System.Text.Json.Serialization;
using AutoMapper;
using BoostProject.Data.Entities.AppUsers;

namespace BoostProject.Services.UserAccountService.Models;

public class RegisterUserAccountResponse
{
    public string? Email { get; set; }
    public string? UserId { get; set; }
}

public class RegisterUserAccountResponseProfile : Profile
{
    public RegisterUserAccountResponseProfile()
    {
        CreateMap<AppUser, RegisterUserAccountResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));
    }
}