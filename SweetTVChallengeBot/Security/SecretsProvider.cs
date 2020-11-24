using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace SweetTVChallengeBot.Security
{
    public class SecretsProvider : ISecretsProvider
    {
        private readonly BotConfiguration _configuration;

        private readonly Dictionary<string, KeyVaultSecret> _secrects = new Dictionary<string, KeyVaultSecret>();

        public SecretsProvider(IOptions<BotConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public string GetValue(string name)
        {
            if (_secrects.ContainsKey(name))
            {
                return _secrects[name].Value;
            }
            SecretClient sc = new SecretClient(new Uri(_configuration.KeyVaultUri), new DefaultAzureCredential());
            KeyVaultSecret secret = sc.GetSecret(name);
            _secrects[name] = secret;

            return secret.Value;
        }
    }
}
