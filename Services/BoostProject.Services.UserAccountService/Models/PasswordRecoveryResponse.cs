using AutoMapper;
using BoostProject.Data.Entities.AppUsers;
using System.Text.Json.Serialization;

namespace BoostProject.Services.UserAccountService.Models;

public class PasswordRecoveryResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class PasswordRecoveryResponseProfile : Profile 
{
    public PasswordRecoveryResponseProfile()
    {
        CreateMap<AppUser, PasswordRecoveryResponse>();
        CreateMap<SendPasswordRecoveryModel, PasswordRecoveryResponse>();
    }
}