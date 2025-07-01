using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client.Pages.Shared
{
    public partial class SkillsSection
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;

        private bool _langReady = false;
        record Skill(string Name, int Level);
        record NavSection(string Title, string Link, string IconClass);

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

        List<Skill> SkillList => new()
        {
            new("C#", 82),
            new("ASP.NET Core", 79),
            new("Entity Framework", 80),
            new("RESTful API", 80),
            new("SQL / PostgreSQL", 75),
            new("Azure DevOps", 70),
        };
    }
}
