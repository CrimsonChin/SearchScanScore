using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<CollectedItem>> GetCollectedItemsAsync(string gameCode, string teamCode)
        {
            var team = await _context.Teams
                .Where(x => x.Code == teamCode
                            && x.Game.Code == gameCode)
                .Include(x => x.CollectedItems)
                .ThenInclude(collectedItem => collectedItem.CollectableItem)
                .FirstOrDefaultAsync();

            return team?.CollectedItems ?? Enumerable.Empty<CollectedItem>();
        }
    }
}
