using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;

namespace Gym.Web.Controllers.Account;

public class LoginController : Controller
{
    // GET
    public IActionResult Index([FromQuery] string ReturnUrl)
    {
        TempData["ReturnUrl"] = ReturnUrl;
        return View();
    }
    public IActionResult AccessDenied()
    {        
        return View();
    }
    public async Task<ActionResult> Login(IFormCollection collection
        , [FromServices] IHandler<Request, Result<Response?>> handler)
    {

        try
        {
            var request = new Request
            {
                Email = collection["email"].ToString(),
                Password = collection["password"].ToString(),
            };
            var result = await handler.HandlerAsync(request);
            ViewData["Email"] = request.Email;
            if (!result.IsSuccess)
            {
                ViewData["ErrorMessage"] = result.Message;
                return View();

            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Data!.Email),
                new Claim(ClaimTypes.Email, result.Data!.Email),
                new Claim("JWToken", result.Data.Token) 
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties { IsPersistent = true });

            if (TempData["ReturnUrl"] != null
                && Url.IsLocalUrl(TempData["ReturnUrl"]!.ToString()))
            {
                var action = TempData["ReturnUrl"]!.ToString().Split('/')[0];
                var controller = TempData["ReturnUrl"]!.ToString().Split('/')[1];
                return RedirectToAction(action,controller);
            }
            return RedirectToAction("Profile", "Account");
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = "Error: Please try later.";
            return View("Index");
        }
    }

}