using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public class StartHandler : BaseHandler, IHandler
    {
        public StartHandler(ITelegramClientFactory client, IOptions<BotConfiguration> configuration) 
            : base(client)
        {
        }

        public async Task HandleAsync(Update update)
        {
            await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "Welcome to Film Bot!");
        }
    }
}
