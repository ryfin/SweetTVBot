using Telegram.Bot;

namespace SweetTVChallengeBot
{
    public interface ITelegramClientFactory
    {
        ITelegramBotClient Create();
    }
}
