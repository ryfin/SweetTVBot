using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Models;
using SweetTVChallengeBot.Storage;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public class HandlerResolver : IHandlerResolver
    {
        private static Dictionary<long, bool> _isDialogStarted = new Dictionary<long, bool>();

        private readonly ITelegramClientFactory _telegramClientFactory;
        private readonly IOptions<BotConfiguration> _configuration;
        private readonly IStorageProvider<Movie> _storageProvider;

        public HandlerResolver(ITelegramClientFactory clientFactory, IOptions<BotConfiguration> configuration, IStorageProvider<Movie> storageProvider)
        {
            _telegramClientFactory = clientFactory;
            _configuration = configuration;
            _storageProvider = storageProvider;
        }

        public IHandler GetHandler(Update update)
        {
            long chatId = update.Message.Chat.Id;
            if (_isDialogStarted.ContainsKey(chatId) && _isDialogStarted[chatId])
            {
                _isDialogStarted[chatId] = false;
                return new DeleteHandler(_telegramClientFactory, _configuration, _storageProvider);
            }

            switch (update.Message.Text)
            {
                case "/start":
                    return new StartHandler(_telegramClientFactory, _configuration);
                case "/list":
                    return new ListHandler(_storageProvider, _telegramClientFactory, _configuration);
                case "/delete":
                    _isDialogStarted[chatId] = true;
                    return new DeleteHandler(_telegramClientFactory, _configuration, _storageProvider);
                default:
                    return new AddMoviewHandler(_storageProvider, _telegramClientFactory, _configuration);
            }
        }
    }
}
