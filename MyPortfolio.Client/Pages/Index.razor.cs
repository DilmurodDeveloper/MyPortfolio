using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MyPortfolio.Client.Pages
{
    public partial class Index
    {
        [Inject] public IJSRuntime JS { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("observeAndStoreSection");
                await JS.InvokeVoidAsync("setupScrollSnap");

                var hash = await JS.InvokeAsync<string>("eval", "location.hash.substring(1)");
                if (!string.IsNullOrEmpty(hash))
                {
                    await JS.InvokeVoidAsync("scrollToSectionById", hash);
                }
            }
        }

        private async Task ScrollToSection(string id)
        {
            await JS.InvokeVoidAsync("scrollToSectionById", id);
        }
    }
}
