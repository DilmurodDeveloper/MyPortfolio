using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyPortfolio.Shared.DTOs.ContactMessages;

namespace MyPortfolio.Client.Pages.Admin.Pages
{
    public partial class AdminMessages
    {
        [Inject] private IHttpClientFactory HttpFactory { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;

        private List<ContactMessageDto>? messages;
        private HttpClient Api => HttpFactory.CreateClient("AuthorizedClient");

        protected override async Task OnInitializedAsync()
        {
            await LoadMessages();
        }

        private async Task LoadMessages()
        {
            messages = await Api.GetFromJsonAsync<List<ContactMessageDto>>("api/contactmessages");
        }

        private async Task Delete(int id)
        {
            var confirmed = await JS.InvokeAsync<bool>("confirm", "Delete this message?");
            if (!confirmed) return;

            var result = await Api.DeleteAsync($"api/contactmessages/{id}");
            if (result.IsSuccessStatusCode)
            {
                await LoadMessages();
            }
            else
            {
                Console.WriteLine($"❌ Delete failed: {result.StatusCode}");
            }
        }
    }
}
