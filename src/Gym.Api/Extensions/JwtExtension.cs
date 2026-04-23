using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gym.Application.Contexts.AccountContext.UseCases.Authenticate;
using Gym.Domain;
using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.IdentityModel.Tokens;

namespace Gym.Api.Extensions;

public class JwtExtension
{
    public static string Generate(Response data)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credentials,
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Response data)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim( new Claim("id", data.Id));
        ci.AddClaim(new Claim(ClaimTypes.GivenName, data.Name));
        ci.AddClaim(new Claim(ClaimTypes.Name, data.Email));
        ci.AddClaim(new Claim(ClaimTypes.Email, data.Email));
        foreach (var role in data.Roles)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        return ci;
    }
}