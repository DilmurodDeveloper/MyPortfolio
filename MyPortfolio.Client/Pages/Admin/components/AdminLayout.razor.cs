namespace MyPortfolio.Client.Pages.Admin.components
{
    public partial class AdminLayout
    {
        private bool isSidebarHidden = false;

        private void ToggleSidebar()
        {
            isSidebarHidden = !isSidebarHidden;
        }
    }
}
