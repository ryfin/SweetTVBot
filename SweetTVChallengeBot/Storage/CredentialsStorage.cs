using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SweetTVChallengeBot.Storage
{
    public class CredentialsStorage : IStorageProvider<ICredentials>
    {
        private static Dictionary<long, HashSet<ICredentials>> _storage = new Dictionary<long, HashSet<ICredentials>>();

        public Task AddAsync(long userId, ICredentials item)
        {
            CheckUserStorageExists(userId);
            _storage[userId].Add(item);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<ICredentials>> GetAllAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICredentials> GetByNameAsync(long userId, string name)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(long userId, string name)
        {
            throw new NotImplementedException();
        }

        private void CheckUserStorageExists(long userId)
        {

        }
    }
}
