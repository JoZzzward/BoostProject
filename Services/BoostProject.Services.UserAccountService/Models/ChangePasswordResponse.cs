using AutoMapper;
using BoostProject.Data.Entities.AppUsers;
using System.Text.Json.Serialization;

namespace BoostProject.Services.UserAccountService.Models;

public class ChangePasswordResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class ChangePasswordResponseProfile : Profile
{
    public ChangePasswordResponseProfile()
    {
        CreateMap<AppUser, ChangePasswordResponse>();
    }
}
