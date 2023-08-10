using BoostProject.Common.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;

namespace BoostProject.ResourceServer.Controllers;

[ApiController]
[Route("resources")]
[EnableCors(PolicyName = CorsConsts.DefaultOriginName)]
[Authorize]
public class ResourceController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetSecretResources()
    {
        var claims = from c in HttpContext.User.Claims
                     where c.Type != "aud" || c.Type != "oi_aud"
                     select new
                     {
                         subject = c.Subject.Name,
                         type = c.Type,
                         value = c.Value
                     };

        return Ok(claims);
    }
}