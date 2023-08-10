using BoostProject.Common.Enums;
using BoostProject.Common.Security;
using BoostProject.Data.Entities.AppUsers;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Primitives;
using OpenIddict.Abstractions;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace BoostProject.AuthorizationServer;

public class AuthService
{
    public static List<string> GetDestinations(Claim claim)
    {
        var destinations = new List<string>();

        if (claim.Type is Claims.Name or Claims.Email)
            destinations.Add(Destinations.AccessToken);

        return destinations;
    }

    public async Task SetResources(IOpenIddictScopeManager scopeManager, ClaimsIdentity identity)
    {
        var resources = await scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync();

        var additional = ApiResourcesManager.GetAllResources();

        resources.AddRange(additional);

        identity.SetResources(resources);
    }

    public async Task SetClaims(UserManager<AppUser> _userManager,
                                AppUser user,
                                ClaimsIdentity identity,
                                string? userId = null)
    {
        userId ??= await _userManager.GetUserIdAsync(user);

        var roles = (await _userManager.GetRolesAsync(user)).ToImmutableArray();
        
        roles = (roles.Length == 0) ? roles.Add(nameof(UserPermissions.User)) : roles;

        identity.SetClaim(Claims.Subject, userId)
                .SetClaim(Claims.Email, await _userManager.GetEmailAsync(user))
                .SetClaim(Claims.Name, await _userManager.GetUserNameAsync(user))
                .SetClaims(Claims.Role, roles);
    }

    public async Task SetAuthorizationId(IOpenIddictAuthorizationManager authorizationManager,
                                         IOpenIddictApplicationManager applicationManager,
                                         OpenIddictRequest request,
                                         object application,
                                         ClaimsIdentity identity,
                                         string userId)
    {
        var client = await applicationManager.GetIdAsync(application);

        var authorizations = await authorizationManager
            .FindAsync(
                subject: userId,
                client: client!,
                status: Statuses.Valid,
                type: AuthorizationTypes.Permanent,
                scopes: request.GetScopes())
            .ToListAsync();

        var authorization = authorizations.LastOrDefault();
        authorization ??= await authorizationManager.CreateAsync(
            identity: identity,
            subject: userId,
            client: client,
            type: AuthorizationTypes.Permanent,
            scopes: identity.GetScopes());

        identity.SetAuthorizationId(await authorizationManager.GetIdAsync(authorization));
    }

    public string BuildRedirectUrl(HttpRequest request, IDictionary<string, StringValues> parameters)
    {
        var url = request.PathBase + request.Path + QueryString.Create(parameters);
        return url;
    }

    public IDictionary<string, StringValues> ParseOAuthParameters(HttpContext httpContext, List<string?> excluding = null)
    {
        excluding ??= new List<string>()!;

        var parameters = httpContext.Request.HasFormContentType 
            ? httpContext.Request.Form
                .Where(parameter => !excluding.Contains(parameter.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value) 
            : httpContext.Request.Query
                .Where(parameter => !excluding.Contains(parameter.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        return parameters;
    }

    public bool IsAuthenticated(AuthenticateResult authenticateResult, OpenIddictRequest request)
    {
        if (!authenticateResult.Succeeded)
            return false;

        if (request.MaxAge.HasValue && authenticateResult.Properties != null)
        {
            var maxAgeSeconds = TimeSpan.FromSeconds(request.MaxAge.Value);

            var expired = !authenticateResult.Properties.IssuedUtc.HasValue ||
                DateTimeOffset.UtcNow - authenticateResult.Properties.IssuedUtc > maxAgeSeconds;

            if (expired) 
                return false;
        }

        return true;
    }
}
