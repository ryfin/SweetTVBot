using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Handlers;
using SweetTVChallengeBot.Security;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Controllers
{
    [Route("api/[controller]")]
    public class BotController : Controller
    {
        private readonly IHandlerResolver _resolver;
        private readonly ISecretsProvider _secretsProvider;
        private readonly BotConfiguration _configuration;

        public BotController(IHandlerResolver handlerResolver, ISecretsProvider secretsProvider, IOptions<BotConfiguration> configuration)
        {
            _resolver = handlerResolver;
            _secretsProvider = secretsProvider;
            _configuration = configuration.Value;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> Update([FromRoute]string token, [FromBody]Update update)
        {
            if (token == _secretsProvider.GetValue(_configuration.TokenName))
            {
                IHandler handler = _resolver.GetHandler(update);
                await handler.HandleAsync(update);

                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}
