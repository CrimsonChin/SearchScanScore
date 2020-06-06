using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface ICollectedItemRepository : IRepository
    {
        CollectedItem Add(CollectedItem collectedItem);

        Task<IEnumerable<CollectedItem>> GetCollectedItemsAsync(string gameCode, string teamCode);
    }
}