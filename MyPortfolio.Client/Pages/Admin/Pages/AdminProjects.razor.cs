using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Shared.DTOs.Projects;

namespace MyPortfolio.Client.Pages.Admin.Pages
{
    public partial class AdminProjects
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IHttpClientFactory HttpFactory { get; set; } = default!;

        private bool showForm = false;

        private List<ProjectDto>? projects;
        private HttpClient Api => HttpFactory.CreateClient("AuthorizedClient");

        private CreateProjectDto projectForm = new();
        private bool isEditMode = false;
        private int editId = 0;

        protected override async Task OnInitializedAsync() => await LoadProjects();

        private async Task LoadProjects()
        {
            projects = await Api.GetFromJsonAsync<List<ProjectDto>>("api/projects");
        }

        private async Task HandleSave()
        {
            if (string.IsNullOrWhiteSpace(projectForm.Title)) return;

            HttpResponseMessage response;

            if (isEditMode)
            {
                var updateDto = new UpdateProjectDto
                {
                    Title = projectForm.Title,
                    Description = projectForm.Description,
                    GithubLink = projectForm.GithubLink,
                    ImageUrl = projectForm.ImageUrl
                };

                response = await Api.PutAsJsonAsync($"api/projects/{editId}", updateDto);
            }
            else
            {
                response = await Api.PostAsJsonAsync("api/projects", projectForm);
            }

            if (response.IsSuccessStatusCode)
            {
                await LoadProjects();
                ResetForm();
            }
            else
            {
                Console.WriteLine($"❌ Save failed: {response.StatusCode}");
            }
        }

        private void LoadForEdit(ProjectDto p)
        {
            projectForm = new CreateProjectDto
            {
                Title = p.Title,
                Description = p.Description,
                GithubLink = p.GithubLink,
                ImageUrl = p.ImageUrl
            };
            editId = p.Id;
            isEditMode = true;
            showForm = true;
        }

        private void CancelEdit() => ResetForm();

        private async Task Delete(int id)
        {
            var confirm = await JS.InvokeAsync<bool>("confirm", "Delete this project?");
            if (!confirm) return;

            var result = await Api.DeleteAsync($"api/projects/{id}");
            if (result.IsSuccessStatusCode)
            {
                await LoadProjects();
            }
            else
            {
                Console.WriteLine($"❌ Delete failed: {result.StatusCode}");
            }
        }

        private void ToggleForm()
        {
            showForm = !showForm;
            if (!showForm)
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            projectForm = new();
            isEditMode = false;
            editId = 0;
        }
    }
}
