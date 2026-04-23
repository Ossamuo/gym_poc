using Gym.Domain.Contexts.AccountContext.UseCases.Create;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Contexts.AccountContext.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
namespace Gym.Web.Controllers.Account
{
    public class AccountController : Controller
    {
        // GET: CreateMemberController
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        
   
        public ActionResult Created(Response response)
        {
            return View(response);
        }
 

        // POST: CreateMemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection, [FromServices] IHandler<Request, Result<Response?>> handler)
        {
            try
            {
                var request = new Request
                {
                    Email = collection["email"].ToString(),
                    Password = collection["password"].ToString(),
                    Name = collection["name"].ToString(),
                };
                var result = await handler.HandlerAsync(request);
                ViewData["Name"] = request.Name;
                ViewData["Email"] = request.Email;
                if (!result.IsSuccess)
                {
                    ViewData["ErrorMessage"] = result.Message;
                    return View();

                }
                return RedirectToAction("Created", result.Data);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: Please try later." ;
                return View();
            }
        }


    }
}
