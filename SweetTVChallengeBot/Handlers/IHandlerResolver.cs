using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public interface IHandlerResolver
    {
        IHandler GetHandler(Update update);
    }
}
