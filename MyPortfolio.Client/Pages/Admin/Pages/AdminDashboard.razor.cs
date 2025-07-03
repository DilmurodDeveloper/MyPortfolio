using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MyPortfolio.Shared.DTOs.Blogs;
using MyPortfolio.Shared.DTOs.ContactMessages;
using MyPortfolio.Shared.DTOs.Projects;

namespace MyPortfolio.Client.Pages.Admin.Pages
{
    public partial class AdminDashboard
    {
        [Inject] private IHttpClientFactory HttpFactory { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        private HttpClient Api => HttpFactory.CreateClient("AuthorizedClient");

        private int TotalProjects;
        private int TotalBlogs;
        private int TotalMessages;

        private List<ProjectDto> LatestProjects = new();
        private List<BlogDto> LatestBlogs = new();
        private List<ContactMessageDto> LatestMessages = new();

        protected override async Task OnInitializedAsync()
        {
            var projectResponse = await Api.GetFromJsonAsync<List<ProjectDto>>("api/projects");
            var blogResponse = await Api.GetFromJsonAsync<List<BlogDto>>("api/blogs");
            var messageResponse = await Api.GetFromJsonAsync<List<ContactMessageDto>>("api/contactmessages");

            TotalProjects = projectResponse?.Count ?? 0;
            TotalBlogs = blogResponse?.Count ?? 0;
            TotalMessages = messageResponse?.Count ?? 0;

            LatestProjects = projectResponse?.Take(5).ToList() ?? new();
            LatestBlogs = blogResponse?.Take(5).ToList() ?? new();
            LatestMessages = messageResponse?.Take(5).ToList() ?? new();
        }

        private void NavigateTo(string url) => Nav.NavigateTo(url);
    }
}
