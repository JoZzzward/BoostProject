using AutoMapper;
using BoostProject.Data.Entities.AppUsers;

namespace BoostProject.Services.UserAccountService.Models;

public class LoginUserAccountResponse
{
    public Guid? UserId { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public string ImageUri { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
}

public class LoginUserAccountResponseProfile : Profile
{
	public LoginUserAccountResponseProfile()
	{
		CreateMap<LoginUserAccountModel, LoginUserAccountResponse>();
		CreateMap<AppUser, LoginUserAccountResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.Id));
	}
}
