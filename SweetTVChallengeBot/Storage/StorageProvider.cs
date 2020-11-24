using SweetTVChallengeBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetTVChallengeBot.Storage
{
    public class StorageProvider : IStorageProvider<Movie>
    {
        private static Dictionary<long, HashSet<Movie>> _movies = new Dictionary<long, HashSet<Movie>>();

        public Task<Movie> GetByNameAsync(long userId, string name)
        {
            CheckUserStorageExists(userId);
            return Task.FromResult(_movies[userId].FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase)));
        }

        public Task<IEnumerable<Movie>> GetAllAsync(long userId)
        {
            CheckUserStorageExists(userId);
            return Task.FromResult(_movies[userId].Select(c => c));
        }

        public Task AddAsync(long userId, Movie item)
        {
            CheckUserStorageExists(userId);
            _movies[userId].Add(item);

            return Task.CompletedTask;
        }

        public Task RemoveAsync(long userId, string name)
        {
            CheckUserStorageExists(userId);
            _movies[userId].Remove(_movies[userId].First(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase)));

            return Task.CompletedTask;
        }

        private void CheckUserStorageExists(long userId)
        {
            if (!_movies.ContainsKey(userId))
            {
                _movies[userId] = new HashSet<Movie>();
            }
        }
    }
}
