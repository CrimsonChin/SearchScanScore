using System.Collections.Generic;
using System.Linq;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Repositories;
using CodeHunt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeHunt.Infrastructure.Repositories
{
    public class CollectedItemRepository : ICollectedItemRepository
    {
        private readonly CodeHuntContext _context;

        public CollectedItemRepository(CodeHuntContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public CollectedItem Add(CollectedItem collectedItem)
        {
            return _context.CollectedItems.Add(collectedItem).Entity;
        }

        public IEnumerable<CollectedItem> GetCollectedItems(string gameExternalId, string teamExternalId)
        {
            var team = _context.Teams
                .Where(x => x.ExternalId == teamExternalId
                            && x.Game.ExternalId == gameExternalId)
                .Include(x => x.CollectedItems)
                .ThenInclude(collectedItem => collectedItem.CollectableItem)
                .FirstOrDefault();

            return team?.CollectedItems ?? Enumerable.Empty<CollectedItem>();
        }
    }
}
