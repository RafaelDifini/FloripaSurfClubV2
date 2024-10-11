using Microsoft.AspNetCore.Components.Authorization;

namespace FloripaSurfClubWeb.Security
{
    public interface ICookieAuthenticationStateProvider
    {
        Task<bool> CheckAuthenticatedAsync();
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void NotifyAuthenticationStateChanged();

    }
}
