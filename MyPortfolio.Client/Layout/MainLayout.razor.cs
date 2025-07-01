using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;

        private bool HideNavbar => Nav.Uri.Contains("/admin/login");

        protected override async Task OnInitializedAsync()
        {
            var culture = await JS.InvokeAsync<string>("localStorage.getItem", "lang");
            await Lang.LoadAsync(culture ?? "en-US");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("hideLoader");
            }
        }
    }
}
