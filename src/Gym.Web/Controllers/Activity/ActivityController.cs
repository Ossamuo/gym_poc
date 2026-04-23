using Gym.Domain.Contexts.ActivitiesContext.UseCases.List;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers.Activity
{
    public class ActivityController : Controller
    {
        [Authorize]
        public async Task<ActionResult> Index([FromServices] IHandler<Request, Result<List<Response>?>> handler)
        {
            if (User.FindFirst("JWToken")?.Value is not string token)
                return RedirectToAction("Login", "Index");

            var request = new Request
            {
                Token = token,
                StartAt = DateTime.Now
            };

            var result = await handler.HandlerAsync(request);
            TempData["BaseUrl"] = Configuration.ImageUrl;
            return View("~/Views/Activity/List/Index.cshtml", result.Data);
        }
    }
}
