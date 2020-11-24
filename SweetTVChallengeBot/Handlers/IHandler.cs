using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SweetTVChallengeBot.Handlers
{
    public interface IHandler
    {
        Task HandleAsync(Update update);
    }
}
