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
                await JS.InvokeVoidAsync("setupSectionAnimations");
                await JS.InvokeVoidAsync("scrollToHash");
                await JS.InvokeVoidAsync("enableScrollHashUpdate");
            }
        }

        private async Task ScrollToTop()
        {
            await JS.InvokeVoidAsync("scrollToTop");
        }
    }
}
