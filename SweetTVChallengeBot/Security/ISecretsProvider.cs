namespace SweetTVChallengeBot.Security
{
    public interface ISecretsProvider
    {
        string GetValue(string secretName);
    }
}
