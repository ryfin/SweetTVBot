using System.Collections.Generic;
using System.Threading.Tasks;

namespace SweetTVChallengeBot.Storage
{
    public interface IStorageProvider<T>
    {
        Task<T> GetByNameAsync(long userId, string name);
        Task<IEnumerable<T>> GetAllAsync(long userId);
        Task AddAsync(long userId, T item);

        Task RemoveAsync(long userId, string name);
    }
}
