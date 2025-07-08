using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Client.Services;

namespace MyPortfolio.Client
{
    public partial class App : ComponentBase
    {
        [Inject] public IJSRuntime JS { get; set; }
        [Inject] public LanguageService? Lang { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var lang = await JS.InvokeAsync<string>("localStorage.getItem", "lang") ?? "en";
            await Lang!.LoadAsync(lang);
        }
    }
}
