using Gym.Domain.Contexts.AccountContext.UseCases.Create.Events;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using SendEmailValidation = Gym.Application.Contexts.AccountContext.UseCases.EmailValidation.HandlerNotification;
using SendMarketingMail = Gym.Application.Contexts.MarketingContext.Events.UserCreated.HandlerNotification;
using MemberBookSession = Gym.Application.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.HandlerNotification;
namespace Gym.Api.Extensions
{
    public static class Notifications
    {
        public static void AddNotificationsEvents(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INotificationHandler<MemberCreatedEvent>, SendEmailValidation>();
            builder.Services.AddScoped<INotificationHandler<MemberCreatedEvent>, SendMarketingMail>();
            builder.Services.AddScoped<INotificationHandler<MemberBookingSessionEvent>, MemberBookSession>();

        }

    }
}
