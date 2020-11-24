using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Models;
using SweetTVChallengeBot.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public class ListHandler : BaseHandler, IHandler
    {
        private readonly IStorageProvider<Movie> _storageProvider;

        public ListHandler(IStorageProvider<Movie> storageProvider, ITelegramClientFactory client, IOptions<BotConfiguration> configuration)
            : base(client)
        {
            _storageProvider = storageProvider;
        }

        public async Task HandleAsync(Update update)
        {
            long userId = update.Message.Chat.Id;
            IEnumerable<Movie> movies = await _storageProvider.GetAllAsync(userId);
            if (movies.Any())
            {
                await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"Your movies to watch:");
                await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, string.Join("\n", (await _storageProvider.GetAllAsync(userId)).Select(c => c.Name)));
            }
            else
            {
                await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"Your don't have any movies in favourites yet");
            }
        }
    }
}
