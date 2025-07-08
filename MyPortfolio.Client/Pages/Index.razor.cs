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
                await JS.InvokeVoidAsync("setupScrollTopButton");
            }
        }

        private async Task ScrollToTop()
        {
            await JS.InvokeVoidAsync("scrollToTop");
        }
    }
}
