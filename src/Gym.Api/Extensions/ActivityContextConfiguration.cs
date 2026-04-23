namespace Gym.Api.Extensions
{
    public static class ActivityContextConfiguration
    {
        public static void AddActivityContextConfiguration(this WebApplicationBuilder builder)
        {

            builder.Services
            .AddTransient<
                Gym.Domain.Contexts.ActivitiesContext.UseCases.List.Contracts.IRepository
                , Gym.Infrastructure.Contexts.ActivitiesContext.UseCases.List.Repository>();
            
            builder.Services
                .AddTransient<
                    Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession.Contracts.IRepository
                    , Gym.Infrastructure.Contexts.ActivitiesContext.UseCases.BookingSession.Repository>();


        }

    }
}
