using Gym.Domain.Contexts.AccountContext.UseCases.Detail;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Gym.Web.Controllers.Account
{
    [Route("Account")]
    public class AccountProfileController : Controller
    {
        // GET: AccountProfileController
        [HttpGet("Profile")] // A URL final será: /Account/Profile
        [Authorize]
        public async Task<ActionResult> Index([FromServices] IHandler<Request, Result<Response?>> handler)
        {
            if (User.FindFirst("JWToken")?.Value is string token)
            {
                var request = new Request();
                request.Token = token;

                var result = await handler.HandlerAsync(request);

                return View("~/Views/Account/Profile/Index.cshtml", result.Data);
            }
            else
                return RedirectToAction("Login", "Index");



        }

        // POST: AccountDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
