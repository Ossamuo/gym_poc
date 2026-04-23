using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Web.Controllers.Logout
{
    public class LogoutController : Controller
    {
        [HttpPost] // Recomendado usar POST por segurança
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Limpa o cookie de autenticação do esquema padrão
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redireciona o usuário para a Home ou para a tela de Login
            return RedirectToAction("Index", "Home");
        }
    }
}
