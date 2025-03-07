using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TgBot.Services;

namespace TgBot.Controllers
{
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
            [FromBody] Update update)
        {
            await handleUpdateService.EchoAsync(update);
            return Ok();
        }
    }
}