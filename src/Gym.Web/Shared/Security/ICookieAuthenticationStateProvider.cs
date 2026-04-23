using Microsoft.AspNetCore.Components.Authorization;

namespace Gym.Web.Shared.Security
{
    public interface ICookieAuthenticationStateProvider
    {
        Task<bool> CheckAuthenticatedAsync();

      
        Task<AuthenticationState> GetAuthenticationStateAsync();

        void NotifyAuthenticationStateChanged();
    }
}
