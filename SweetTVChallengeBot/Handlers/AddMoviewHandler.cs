using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Models;
using SweetTVChallengeBot.Storage;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public class AddMoviewHandler : BaseHandler, IHandler
    {
        private readonly IStorageProvider<Movie> _storageProvider;

        public AddMoviewHandler(IStorageProvider<Movie> storageProvider, ITelegramClientFactory client, IOptions<BotConfiguration> configuration)
            :base(client)
        {
            _storageProvider = storageProvider;
        }

        public async Task HandleAsync(Update update)
        {
            string moviewName = update.Message.Text;
            long userId = update.Message.Chat.Id;
            if (await _storageProvider.GetByNameAsync(userId, moviewName) == null)
            {
                await _storageProvider.AddAsync(userId, new Movie { Name = moviewName });
                await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"Movie {moviewName} has been added to you favourites. You can find it in your profile");

                return;
            }

            await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"Movie {moviewName} already in your favorites with");
        }
    }
}
