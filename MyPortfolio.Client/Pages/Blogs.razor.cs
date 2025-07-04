using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;
using MyPortfolio.Shared.DTOs.Blogs;

namespace MyPortfolio.Client.Pages
{
    public partial class Blogs
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;
        [Inject] public IHttpClientFactory HttpFactory { get; set; } = default!; private List<BlogDto>? blogs;

        private bool _langReady = false;

        protected override async Task OnInitializedAsync()
        {
            var client = HttpFactory.CreateClient("AuthorizedClient");
            blogs = await client.GetFromJsonAsync<List<BlogDto>>("api/blogs");

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

        private string Truncate(string? text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            return text.Length <= maxLength ? text : text.Substring(0, maxLength) + "...";
        }
    }
}