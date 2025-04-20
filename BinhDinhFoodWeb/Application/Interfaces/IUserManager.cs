using System.Security.Claims;
using BinhDinhFood.Presentation.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace BinhDinhFood.Application.Interfaces;

public interface IUserManager
{
    public Task SignIn(
        HttpContext httpContext,
        CookieUserItem user,
        bool isPersistent = false);

    public Task SignOut(HttpContext httpContext);
}
public class UserManager : IUserManager
{
    private List<Claim> GetUserClaims(CookieUserItem user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };
        return claims;
    }
    public async Task SignIn(HttpContext httpContext, CookieUserItem user, bool isPersistent = false)
    {
        string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        // Generate Claims from DbEntity
        var claims = GetUserClaims(user);

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, authenticationScheme);

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(
                claimsIdentity);

        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            // Refreshing the authentication session should be allowed.
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.
            IsPersistent = isPersistent,
            // Whether the authentication session is persisted across 
            // multiple requests. Required when setting the 
            // ExpireTimeSpan option of CookieAuthenticationOptions 
            // set with AddCookie. Also required when setting 
            // ExpiresUtc.
            // IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.
            // RedirectUri = "~/Account/Index"
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };

        await httpContext.SignInAsync(
            scheme: "Signin",
            principal: claimsPrincipal,
            properties: authProperties
            );
    }

    public async Task SignOut(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(
        scheme: "Signin");
    }
}
