using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BoostProject.AuthorizationServer.Configuration.Factories;

public class ClaimsFactory<T> : UserClaimsPrincipalFactory<T> where T : IdentityUser<Guid>
{
    private readonly UserManager<T> _userManager;

    public ClaimsFactory(
        UserManager<T> userManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
        _userManager = userManager;
    }
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(T user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        identity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return identity;
    }
}
