using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Pages.Shared
{
    public partial class HomeSection
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        private bool _langReady = false;

        protected override async Task OnInitializedAsync()
        {
            var culture = await JS.InvokeAsync<string>("localStorage.getItem", "lang") ?? "en-US";
            await Lang.LoadAsync(culture);
            _langReady = true;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            Lang.OnLanguageChanged += HandleLanguageChanged;
        }

        private void HandleLanguageChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Lang.OnLanguageChanged -= HandleLanguageChanged;
        }

        protected List<(string Title, string Url, string Icon)> SocialLinks = new()
        {
            ("GitHub", "https://github.com/DilmurodDeveloper", "bi bi-github"),
            ("LinkedIn", "https://linkedin.com/in/dilmurodmadirimov", "bi bi-linkedin"),
            ("Facebook", "https://facebook.com/dilmurod.dev", "bi bi-facebook"),
            ("Instagram", "https://instagram.com/dilmurod_developer", "bi bi-instagram"),
            ("Telegram", "https://t.me/DilmurodDeveloper", "bi bi-telegram"),
            ("WhatsApp", "https://wa.me/+998991437101", "bi bi-whatsapp"),
        };
    }
}
