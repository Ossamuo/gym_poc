using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Contexts.AccountContext.Login;

namespace Gym.Web.Extensions.Services.Login
{
    public static class LoginHandler
    {
        internal static void AddLoginHandler(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IHandler<Request, Result<Response?>>, Handler>();
        }
    }
}
