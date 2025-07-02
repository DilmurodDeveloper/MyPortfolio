using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Pages.Admin.components
{
    public partial class Navbar
    {
        private bool isUserMenuOpen = false;

        [Parameter]
        public EventCallback OnToggleSidebar { get; set; }

        [Inject] NavigationManager Nav { get; set; } = default!;
        [Inject] Blazored.LocalStorage.ILocalStorageService localStorage { get; set; } = default!;
        [Inject] AuthenticationStateProvider authProvider { get; set; } = default!;

        private void ToggleUserMenu() => isUserMenuOpen = !isUserMenuOpen;

        private async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");

            if (authProvider is CustomAuthStateProvider custom)
                custom.NotifyUserLogout();

            Nav.NavigateTo("/admin/login", true);
        }

        private async Task OnToggleSidebarClicked()
        {
            if (OnToggleSidebar.HasDelegate)
                await OnToggleSidebar.InvokeAsync(null);
        }
    }
}
