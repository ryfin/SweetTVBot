using Microsoft.Extensions.Options;
using SweetTVChallengeBot.Security;
using Telegram.Bot;

namespace SweetTVChallengeBot
{
    public class TelegramClientFactory : ITelegramClientFactory
    {
        private readonly BotConfiguration _configuration;
        private readonly ISecretsProvider _secretsProvider;
     
        public TelegramClientFactory(ISecretsProvider secretsProvider, IOptions<BotConfiguration> configuration)
        {
            _secretsProvider = secretsProvider;
            _configuration = configuration.Value;
        }

        public ITelegramBotClient Create()
        {
            string secret = _secretsProvider.GetValue(_configuration.TokenName);

            return new TelegramBotClient(secret);
        }
    }
}
