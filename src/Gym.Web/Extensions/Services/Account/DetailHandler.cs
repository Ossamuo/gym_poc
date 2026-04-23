using Gym.Domain.Contexts.AccountContext.UseCases.Detail;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using MemberDetailHandler = Gym.Web.Contexts.AccountContext.Detail.Handler;

namespace Gym.Web.Extensions.Services.Account
{
    internal static class DetailHandler
    {
        internal static void AddDetailHandler(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IHandler<Request, Result<Response?>>, MemberDetailHandler>();
        }
    }
}
