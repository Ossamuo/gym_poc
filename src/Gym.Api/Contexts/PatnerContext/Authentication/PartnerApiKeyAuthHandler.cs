using Gym.Domain.Contexts.PartnerContext.UseCases.Auth.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Gym.Api.Contexts.PatnerContext.Authentication;

public class PartnerApiKeyAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IRepository partnerRepository)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Api-Key", out var apiKey) ||
            !Request.Headers.TryGetValue("X-Api-Secret", out var apiSecret))
            return AuthenticateResult.NoResult();

        var partner = await partnerRepository.GetByApiKeyAsync(apiKey!, CancellationToken.None);
        if (partner is null)
            return AuthenticateResult.Fail("Invalid API key.");

        if (!partner.ApiSecretHash.Challenge(apiSecret!))
            return AuthenticateResult.Fail("Invalid API secret.");

        var claims = new[] { new Claim("PartnerId", partner.Id.ToString()) };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
