﻿using FloripaSurfClubCore.Handlers;
using FloripaSurfClubWeb.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FloripaSurfClubWeb.Pages.Identity
{
    public partial class LogoutPage : ComponentBase
    {
        #region Dependecies
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IAccountHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        protected override async Task OnInitializedAsync()
        {
            if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
            {
                await Handler.LogoutAsync();
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
            }

            await base.OnInitializedAsync();
        }
    }
}
