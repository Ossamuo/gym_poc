using Gym.Api.Contexts.PatnerContext.Authentication;
using Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Contratcs;
using Microsoft.AspNetCore.Authentication;

namespace Gym.Api.Extensions
{
    public static class PartnerContextConfiguration
    {
        public const string Scheme = "PartnerApiKey";
        public const string Policy = "PartnerApiKeyPolicy";

        public static void AddPartnerContextConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<
                    IRepository,
                    Gym.Infrastructure.Contexts.PartnerContext.UseCases.BookingSession.Repository>();

            builder.Services
                .AddTransient<
                    Gym.Domain.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Constracts.IServices,
                    Gym.Infrastructure.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Service>();

            builder.Services
                .AddTransient<
                    Gym.Domain.Contexts.PartnerContext.UseCases.Auth.Contracts.IRepository,
                    Gym.Infrastructure.Contexts.PartnerContext.UseCases.Auth.Repository>();

            builder.Services
                .AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, PartnerApiKeyAuthHandler>(Scheme, _ => { });

            builder.Services
                .AddAuthorizationBuilder()
                .AddPolicy(Policy, policy => policy
                    .AddAuthenticationSchemes(Scheme)
                    .RequireAuthenticatedUser());
        }
    }
}
