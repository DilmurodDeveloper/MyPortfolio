using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;
using MyPortfolio.Shared.DTOs.Projects;

namespace MyPortfolio.Client.Pages.Shared
{
    public partial class ProjectsSection
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;
        [Inject] public IHttpClientFactory HttpFactory { get; set; } = default!;

        private List<ProjectDto> projects = new();
        private bool _langReady = false;
        private int currentIndex = 0;
        private int visibleCount = 3;
        private int slideWidth = 300;

        protected override async Task OnInitializedAsync()
        {
            var client = HttpFactory.CreateClient("AuthorizedClient");
            projects = await client.GetFromJsonAsync<List<ProjectDto>>("api/projects") ?? new();

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

        private void ScrollLeft()
        {
            if (currentIndex > 0)
                currentIndex--;
            else
                currentIndex = Math.Max(0, projects.Count - visibleCount);
        }

        private void ScrollRight()
        {
            int maxIndex = Math.Max(0, projects.Count - visibleCount);
            if (currentIndex < maxIndex)
                currentIndex++;
            else
                currentIndex = 0;
        }

        private string GetTransformStyle()
        {
            int shift = currentIndex * (slideWidth + 24);
            return $"transform: translateX(-{shift}px);";
        }
    }
}
