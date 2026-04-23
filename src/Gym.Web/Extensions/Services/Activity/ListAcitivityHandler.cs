using Gym.Domain.Contexts.ActivitiesContext.UseCases.List;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Contexts.ActivitiesContext.List;

namespace Gym.Web.Extensions.Services.Activity
{
    public static class ListAcitivityHandler
    {
        internal static void AddListAcitivityHandler(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IHandler<Request, Result<List<Response>?>>, Handler>();
        }
    }
}
