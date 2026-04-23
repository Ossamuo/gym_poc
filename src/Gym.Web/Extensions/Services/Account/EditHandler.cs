using Gym.Domain.Contexts.AccountContext.UseCases.Edit;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Contexts.AccountContext.Edit;

namespace Gym.Web.Extensions.Services.Account
{
    public static class EditHandlerExtensions
    {
        public static void AddEditHandler(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IHandler<Request, Result<Response?>>, Handler>();
        }
    }
}
