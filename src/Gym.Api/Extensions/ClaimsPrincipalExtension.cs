using System.Security.Claims;

namespace Gym.Api.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string Id(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value??string.Empty;
    }
    public static string Name(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value?? string.Empty;
    }
    public static string Email(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value?? string.Empty;
    }
    public static string GiveName(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value?? string.Empty;
    }
}