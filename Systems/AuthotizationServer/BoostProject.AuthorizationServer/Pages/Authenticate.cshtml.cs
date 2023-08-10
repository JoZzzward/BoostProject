using BoostProject.AuthorizationServer.Pages.Authenticate.Models;
using BoostProject.Common.Application_Urls;
using BoostProject.Common.Extensions;
using BoostProject.Services.UserAccountService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace BoostProject.AuthorizationServer.Pages;

public class AuthenticateModel : PageModel
{
    private readonly HttpClient _httpClient;

    public AuthenticateModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [BindProperty]
    public string ReturnUrl { get; set; }

    public string AuthStatus { get; set; } = "Empty";

    public IActionResult OnGet(string returnUrl)
    {
        ReturnUrl = returnUrl;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string login, string password)
    {
        var response = await _httpClient.PostAsJsonAsync(AppUrls.Api.Accounts.Login, 
            new LoginUserAccountRequest()
            {
                Login = login,
                Password = password
            });

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<LoginUserAccountResponse>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (result.UserId.IsNullOrDefault())
        {
            AuthStatus = "Incorrect login or password";
            return Page();
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, result.Email),
            new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
            new Claim(ClaimTypes.Name, result.FirstName),
            new Claim(ClaimTypes.Surname, result.LastName),
            new Claim(ClaimTypes.MobilePhone, result.PhoneNumber ?? "")
        };

        var principal = new ClaimsPrincipal(
            new List<ClaimsIdentity>
            {
                    new(claims, CookieAuthenticationDefaults.AuthenticationScheme)
            });

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        if (!string.IsNullOrEmpty(ReturnUrl))
            return Redirect(ReturnUrl);

        AuthStatus = "Successfully authenticated!";
        return Page();
    }
}
