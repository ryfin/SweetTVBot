using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Models;
using SweetTVChallengeBot.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace SweetTVChallengeBot.Handlers
{
    public class DeleteHandler : BaseHandler, IHandler
    {
        private readonly IStorageProvider<Movie> _storageProvider;

        public DeleteHandler(ITelegramClientFactory client, IOptions<BotConfiguration> configuration, IStorageProvider<Movie> storageProvider)
            : base(client)
        {
            _storageProvider = storageProvider;
        }

        public async Task HandleAsync(Update update)
        {
            string message = update.Message.Text;
            long userId = update.Message.Chat.Id;
            if (message == "/delete")
            {
                IEnumerable<Movie> movies = await _storageProvider.GetAllAsync(userId);
                if (movies.Any())
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup
                    {
                        Keyboard = (await _storageProvider.GetAllAsync(userId)).Select(c => new List<KeyboardButton> { new KeyboardButton(c.Name) }),
                        OneTimeKeyboard = true
                    };
                    await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "Which movie you want to delete?", Telegram.Bot.Types.Enums.ParseMode.Default,
                        false, false, 0, replyKeyboardMarkup);
                }
                else
                {
                    await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"Your don't have any movies in favourites yet");
                }
            }
            else
            {
                await _storageProvider.RemoveAsync(userId, message);
                await TelegramBotClient.SendTextMessageAsync(update.Message.Chat.Id,
                    $"Movie {message} wasn removed from your favourites",
                    Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0,
                    new ReplyKeyboardRemove());
            }
        }
    }
}
