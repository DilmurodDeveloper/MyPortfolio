using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Pages.Admin
{
    public partial class AdminDashboard
    {
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public ILocalStorageService localStorage { get; set; } = default!;
        [Inject] public AuthenticationStateProvider authProvider { get; set; } = default!;

        private bool isSidebarHidden = false;
        private bool isUserMenuOpen = false;

        void ToggleSidebar() => isSidebarHidden = !isSidebarHidden;
        void ToggleUserMenu() => isUserMenuOpen = !isUserMenuOpen;

        private async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");

            if (authProvider is CustomAuthStateProvider custom)
                custom.NotifyUserLogout();

            Nav.NavigateTo("/admin/login", true);
        }
    }
}
