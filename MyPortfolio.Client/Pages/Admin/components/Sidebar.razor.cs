using Microsoft.AspNetCore.Components;

namespace MyPortfolio.Client.Pages.Admin.components
{
    public partial class Sidebar
    {
        [Parameter]
        public bool IsHidden { get; set; }
    }
}
