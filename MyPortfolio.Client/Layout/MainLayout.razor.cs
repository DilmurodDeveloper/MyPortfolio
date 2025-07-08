using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;

        private string? _currentUri;

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


        protected override void OnInitialized()
        {
            _currentUri = Nav.Uri;
            Nav.LocationChanged += HandleLocationChanged;
        }

        private async void HandleLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            var path = new Uri(e.Location).AbsolutePath.ToLower();

            bool isAdminPage = path == "/admin/projects"
                        || path == "/admin/blogs"
                        || path == "/admin/dashboard"
                        || path == "/admin/messages";

            if (!isAdminPage)
            {
                await JS.InvokeVoidAsync("showLoader");
                await Task.Delay(300);
                await JS.InvokeVoidAsync("hideLoader");
            }

            _currentUri = e.Location;
        }

        public void Dispose()
        {
            Nav.LocationChanged -= HandleLocationChanged;
        }
    }
}