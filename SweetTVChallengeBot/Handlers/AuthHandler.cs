using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Storage;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public class AuthHandler : BaseHandler, IHandler
    {
        private static IStorageProvider<ICredentials> _storageProvider;

        public AuthHandler(ITelegramClientFactory client, IOptions<BotConfiguration> configuration, IStorageProvider<ICredentials> storageProvider) 
            : base(client)
        {
            _storageProvider = storageProvider;
        }

        public async Task HandleAsync(Update update)
        {
            await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "Here, authorisation via OAuth should be performed");
        }
    }
}
