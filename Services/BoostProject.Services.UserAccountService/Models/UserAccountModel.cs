using AutoMapper;
using BoostProject.Data.Entities.AppUsers;

namespace BoostProject.Services.UserAccountService.Models;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<AppUser, UserAccountModel>();
    }
}
