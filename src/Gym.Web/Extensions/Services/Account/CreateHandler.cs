using Gym.Domain.Contexts.AccountContext.UseCases.Create;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using MemberCreateHandler = Gym.Web.Contexts.AccountContext.Create.Handler;

namespace Gym.Web.Extensions.Services.Account
{
    internal static class CreateHandler
    {
        internal static void AddCreateHandler(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IHandler<Request, Result<Response?>>, MemberCreateHandler>();
        }
    }
}
