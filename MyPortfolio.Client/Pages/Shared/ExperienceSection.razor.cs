using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;
using MyPortfolio.Shared.Models;

namespace MyPortfolio.Client.Pages.Shared
{
    public partial class ExperienceSection : ComponentBase, IDisposable
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;

        private bool _langReady = false;
        private List<ExperienceModel>? ExperienceItems;

        protected override void OnInitialized()
        {
            Lang.OnLanguageChanged += HandleLanguageChanged;
        }

        protected override async Task OnInitializedAsync()
        {
            var culture = await JS.InvokeAsync<string>("localStorage.getItem", "lang") ?? "en-US";
            await Lang.LoadAsync(culture);
            ExperienceItems = BuildExperienceItems();
            _langReady = true;
            StateHasChanged();
        }

        private void HandleLanguageChanged()
        {
            ExperienceItems = BuildExperienceItems();
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Lang.OnLanguageChanged -= HandleLanguageChanged;
        }

        private List<ExperienceModel> BuildExperienceItems() => new()
    {
        new ExperienceModel(
            Lang.T("experience", "company_amusoft"),
            Lang.T("experience", "role_amusoft"),
            Lang.T("experience", "duration_amusoft"),
            Lang.T("experience", "location_amusoft"),
            true),

        new ExperienceModel(
            Lang.T("experience", "company_a1tech"),
            Lang.T("experience", "role_a1tech"),
            Lang.T("experience", "duration_a1tech"),
            Lang.T("experience", "location_a1tech"),
            false),

        new ExperienceModel(
            Lang.T("experience", "company_self"),
            Lang.T("experience", "role_self"),
            Lang.T("experience", "duration_self"),
            Lang.T("experience", "location_self"),
            true)
    };
    }
}
