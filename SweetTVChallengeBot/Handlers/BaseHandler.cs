using Telegram.Bot;

namespace SweetTVChallengeBot.Handlers
{
    public abstract class BaseHandler
    {
        private readonly ITelegramClientFactory _telegramClientFactory;

        private ITelegramBotClient _telegramBotClient;

        public BaseHandler(ITelegramClientFactory clientFactory)
        {
            _telegramClientFactory = clientFactory;
        }
        protected ITelegramBotClient TelegramBotClient
        {
            get
            {
                if(_telegramBotClient == null)
                {
                    _telegramBotClient = _telegramClientFactory.Create();
                }

                return _telegramBotClient;
            }
        }
    }
}
