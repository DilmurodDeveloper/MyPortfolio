using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Shared.DTOs.Blogs;

namespace MyPortfolio.Client.Pages.Admin.Pages
{
    public partial class AdminBlogs : ComponentBase
    {
        [Inject] private IJSRuntime JS { get; set; } = default!;
        [Inject] private IHttpClientFactory HttpFactory { get; set; } = default!;

        private List<BlogDto>? blogs;
        private CreateBlogDto form = new();
        private bool isEdit = false;
        private int editId;
        private HttpClient Api => HttpFactory.CreateClient("AuthorizedClient");

        protected override async Task OnInitializedAsync()
        {
            await LoadBlogs();
        }

        private async Task LoadBlogs()
        {
            blogs = await Api.GetFromJsonAsync<List<BlogDto>>("api/blogs");
        }

        private async Task HandleSave()
        {
            HttpResponseMessage response;

            if (isEdit)
            {
                var dto = new UpdateBlogDto
                {
                    Title = form.Title,
                    Content = form.Content,
                    ImageUrl = form.ImageUrl
                };
                response = await Api.PutAsJsonAsync($"api/blogs/{editId}", dto);
            }
            else
            {
                response = await Api.PostAsJsonAsync("api/blogs", form);
            }

            if (response.IsSuccessStatusCode)
            {
                await LoadBlogs();
                ResetForm();
            }
            else
            {
                Console.WriteLine($"❌ Save failed: {response.StatusCode}");
            }
        }

        private void Edit(BlogDto blog)
        {
            form = new CreateBlogDto
            {
                Title = blog.Title,
                Content = blog.Content,
                ImageUrl = blog.ImageUrl
            };
            isEdit = true;
            editId = blog.Id;
        }

        private async Task Delete(int id)
        {
            var confirmed = await JS.InvokeAsync<bool>("confirm", "Delete this blog post?");
            if (!confirmed) return;

            var result = await Api.DeleteAsync($"api/blogs/{id}");
            if (result.IsSuccessStatusCode)
            {
                await LoadBlogs();
            }
            else
            {
                Console.WriteLine($"❌ Delete failed: {result.StatusCode}");
            }
        }

        private void ResetForm()
        {
            form = new();
            isEdit = false;
            editId = 0;
        }
    }
}
