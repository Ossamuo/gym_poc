using Gym.Domain.Contexts.AccountContext.UseCases.Authenticate;
using Microsoft.AspNetCore.Components.Authorization;
using NuGet.Common;
using System.Security.Claims;

namespace Gym.Web.Shared.Security
{
    public class CookieAuthenticationStateProvider(Response response) :
        AuthenticationStateProvider, ICookieAuthenticationStateProvider
    {

        private bool _isAuthenticated = false;

        public async Task<bool> CheckAuthenticatedAsync()
        {
            await this.GetAuthenticationStateAsync();
            return _isAuthenticated;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _isAuthenticated = false;

            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var claims = await this.GetClaimsAsync(response);
            var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
            user = new ClaimsPrincipal(id);
            _isAuthenticated = true;
            return new AuthenticationState(user);
        }

        public void NotifyAuthenticationStateChanged() => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());


        private async Task<List<Claim>> GetClaimsAsync(Response user)
        {

            var claims = new List<Claim>();
            claims.Add(new(ClaimTypes.Name, user.Email));//toda vez que é utilizado o User.Identity.Name é daqui que é recuperado a informação
            claims.Add(new(ClaimTypes.Email, user.Email));
            claims.Add(new("JWToken", user.Token));            
            claims.AddRange(
                user.Roles.Select(x => new Claim(ClaimTypes.Role, x))
                );
            return claims;
            //claims.AddRange(
            //user.Roles.Where(x =>
            //x.Key != ClaimTypes.Name &&
            //x.Key != ClaimTypes.Email)
            //.Select(x => new Claim(x.Key, x.Value))
            //);

            //RoleClaim[]? roles;

            //try
            //{
            //    roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
            //}
            //catch
            //{
            //    return claims;
            //}

            //foreach (var role in roles ?? [])
            //{
            //    if (!string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value))
            //        claims.Add(new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));
            //}
            //return claims;
        }
    }   
}
