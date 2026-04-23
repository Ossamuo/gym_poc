using Gym.Domain.Contexts.AccountContext.UseCases.Edit;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers.Account
{
    [Route("Account")]
    public class AccountEditController : Controller
    {
        [Authorize]
        [HttpGet("Edit")] // A URL final será: /Account/Edit
        public IActionResult Index(Request request)
        {

            return View("~/Views/Account/Edit/Index.cshtml", request);
        }

        [Authorize]
        [HttpPost("Edit")] // A URL final será: /Account/Edit
        public async Task<IActionResult> Index(IFormCollection collection, [FromServices] IHandler<Request, Result<Response?>> handler)
        {

            if (User.FindFirst("JWToken")?.Value is string token)
            {
                var request = new Request
                {
                    Name = Convert.ToString(collection["Name"]),
                    Image = Convert.ToString(collection["Image"]),
                    Token = token
                };

                var result = await handler.HandlerAsync(request);
                if (!result.IsSuccess)
                {
                    ViewData["ErrorMessage"] = result.Message;
                    return View(request);
                }
                var resulData = new Gym.Domain.Contexts.AccountContext.UseCases.Detail.Response
                (
                    result.Data!.Name,
                    result.Data!.Email,
                    result.Data!.Image
                );
                return View("~/Views/Account/Profile/Index.cshtml", resulData);
            }



            return View("Index", "AccountProfile");
        }
    }
}
