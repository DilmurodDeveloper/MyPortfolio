using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;
using MyPortfolio.Shared.Models;

namespace MyPortfolio.Client.Pages.Shared
{
    public partial class EducationSection : ComponentBase, IDisposable
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;

        private List<EducationItem> EducationList = new();
        protected bool _langReady = false;

        protected override async Task OnInitializedAsync()
        {
            var culture = await JS.InvokeAsync<string>("localStorage.getItem", "lang") ?? "en-US";
            await Lang.LoadAsync(culture);

            EducationList = GetTranslatedEducationItems();
            _langReady = true;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            Lang.OnLanguageChanged += HandleLanguageChanged;
        }

        private void HandleLanguageChanged()
        {
            EducationList = GetTranslatedEducationItems();
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Lang.OnLanguageChanged -= HandleLanguageChanged;
        }

        private List<EducationItem> GetTranslatedEducationItems()
        {
            return new List<EducationItem>
            {
                new()
                {
                    Title = Lang.T("education", "tuit_title"),
                    Image = "/images/education/tuit1.jpg",
                    Degree = Lang.T("education", "tuit_degree"),
                    Field = Lang.T("education", "tuit_field"),
                    Years = Lang.T("education", "tuit_years"),
                    Location = Lang.T("education", "tuit_location")
                },
                new()
                {
                    Title = Lang.T("education", "mohirdev_title"),
                    Image = "/images/education/mohirdev1.png",
                    Degree = Lang.T("education", "mohirdev_degree"),
                    Field = Lang.T("education", "mohirdev_field"),
                    Years = Lang.T("education", "mohirdev_years"),
                    Location = Lang.T("education", "mohirdev_location")
                }
            };
        }
    }
}
