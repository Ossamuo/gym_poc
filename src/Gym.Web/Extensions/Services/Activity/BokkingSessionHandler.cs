using Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Contexts.ActivitiesContext.BookingSession;
namespace Gym.Web.Extensions.Services.Activity
{
    public static class BokkingSessionHandler
    {
        internal static void AddBokkingSessionHandler(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IHandler<Request, Result<Response?>>, Handler>();
        }
    }
}
