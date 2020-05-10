using System.Collections.Generic;
using CodeHunt.Domain.Entities;

namespace CodeHunt.Domain.Repositories
{
    public interface ICollectedItemRepository : IRepository
    {
        CollectedItem Add(CollectedItem collectedItem);

        IEnumerable<CollectedItem> GetCollectedItems(string gameExternalId, string teamExternalId);
    }
}