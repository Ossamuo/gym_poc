using Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers.Activity;

public class ActivityBookingController : Controller
{
    // GET
    // POST: ActivityController/Book
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Book(
        Guid activityId,
        Guid partnerId,
        [FromServices] IHandler<Request, Result<Response?>> handler)
    {
        if (User.FindFirst("JWToken")?.Value is not string token)
            return RedirectToAction("Login", "Index");

        var memberId = Guid.Parse(User.FindFirst("MemberId")?.Value ?? Guid.Empty.ToString());

        var request = new Request
        {
            Token = token,
            ActivityId = activityId,
            PartnerId = partnerId,
            MemberId = memberId
        };

        var result = await handler.HandlerAsync(request);
        if (result.IsSuccess)
            TempData["Message"] = result.Message;
        else
            TempData["ErrorMessage"] = result.Message;
        return RedirectToAction("Index", "Activity", TempData["Message"] ?? TempData["ErrorMessage"]);
    }

}