using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MyPortfolio.Shared.Models;

namespace MyPortfolio.Client.Pages.Shared
{
    public partial class ChatComponent
    {
        private bool isOpen = false;
        private string activeTab = "form";

        private ChatMessage messageModel = new();
        private List<ChatMessage> recentMessages = new();
        private ChatMessage? selectedMessage = null;

        [Inject] public IHttpClientFactory HttpFactory { get; set; } = default!;

        protected override void OnInitialized()
        {
            recentMessages.Add(new ChatMessage
            {
                Name = "Dilmurod",
                Message = "Welcome to my site, if you need help simply reply to this message, I'm online and ready to help."
            });
        }

        private async Task SubmitMessage()
        {
            var client = HttpFactory.CreateClient("AuthorizedClient");
            var response = await client.PostAsJsonAsync("api/chat", messageModel);

            if (response.IsSuccessStatusCode)
            {
                recentMessages.Insert(0, messageModel);
                messageModel = new();
                activeTab = "recent";
            }
        }

        private void SelectMessage(ChatMessage msg)
        {
            selectedMessage = msg;
        }

        private string Truncate(string text, int maxLines)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            var lines = text.Split('\n');
            if (lines.Length > maxLines)
            {
                return string.Join(" ", lines.Take(maxLines)) + "...";
            }
            return text;
        }
    }
}
