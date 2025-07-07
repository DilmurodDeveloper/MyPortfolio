using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Layout
{
    public partial class NavMenu : ComponentBase, IDisposable
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;

        protected bool collapseNavMenu = true;
        private string CurrentLanguage = "en";
        private bool _langReady = false;
        private readonly List<string> SupportedLanguages = new() { "ru", "en", "uz" };

        protected override async Task OnInitializedAsync()
        {
            var culture = await JS.InvokeAsync<string>("localStorage.getItem", "lang") ?? "en-US";
            CurrentLanguage = culture.Split('-')[0];
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

        private async Task CycleLanguage()
        {
            int currentIndex = SupportedLanguages.IndexOf(CurrentLanguage);
            int nextIndex = (currentIndex + 1) % SupportedLanguages.Count;
            CurrentLanguage = SupportedLanguages[nextIndex];
            await ChangeLanguageHandler(CurrentLanguage);
        }

        private async Task ChangeLanguageHandler(string langCode)
        {
            CurrentLanguage = langCode;
            await ChangeLanguage(langCode);
        }

        protected async Task ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
            await JS.InvokeVoidAsync("setScrollLock", !collapseNavMenu);
        }

        protected async Task ChangeLanguage(string lang)
        {
            await JS.InvokeVoidAsync("localStorage.setItem", "lang", lang);
            await Lang.LoadAsync(lang);
            StateHasChanged();
        }

        protected List<(string Title, string Url, string Icon)> SocialLinks = new()
        {
            ("GitHub", "https://github.com/DilmurodDeveloper", "bi bi-github"),
            ("LinkedIn", "https://linkedin.com/in/dilmurodmadirimov", "bi bi-linkedin"),
            ("Facebook", "https://facebook.com/dilmurod.dev", "bi bi-facebook"),
            ("Instagram", "https://instagram.com/dilmurod_developer", "bi bi-instagram"),
            ("Telegram", "https://t.me/DilmurodDeveloper", "bi bi-telegram"),
            ("WhatsApp", "https://wa.me/+998991437101", "bi bi-whatsapp"),
            ("Download CV", "/files/Dilmurod_Resume.pdf", "bi bi-file-earmark-person-fill")
        };
    }
}
