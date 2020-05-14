using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Repositories;
using CodeHunt.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeHunt.Infrastructure.Repositories
{
    public class SightingRepository : ISightingRepository
    {
        private readonly CodeHuntContext _context;

        public SightingRepository(CodeHuntContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Sighting Add(Sighting sighting)
        {
            return _context.Sightings.Add(sighting).Entity;
        }

        public async Task<IEnumerable<Sighting>> GetAsync(string gameExternalId, string teamExternalId)
        {
            var team = await _context.Teams
                .Where(x => x.ExternalId == teamExternalId
                            && x.Game.ExternalId == gameExternalId)
                .Include(x => x.Sightings)
                .ThenInclude(y => y.Guard)
                .FirstOrDefaultAsync();

            return team?.Sightings ?? Enumerable.Empty<Sighting>();
        }
    }
}
