using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Shared.Models;
using RESTFulSense.Controllers;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : RESTFulController
    {
        private readonly IConfiguration configuration;

        public ChatController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage model)
        {
            var botToken = configuration["Telegram:BotToken"];
            var chatId = configuration["Telegram:ChatId"];

            var messageText = $"📥 *Yangi xabar!*\n\n👤 Ism: {model.Name}\n📧 Email: {model.Email}\n💬 Xabar:\n{model.Message}";

            var url = $"https://api.telegram.org/bot{botToken}/sendMessage";
            var payload = new
            {
                chat_id = chatId,
                text = messageText,
                parse_mode = "Markdown"
            };

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync(url, payload);

            if (response.IsSuccessStatusCode)
                return Ok(new { success = true });

            return BadRequest(new { error = "Telegramga yuborilmadi." });
        }
    }

}
