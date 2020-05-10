using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Sighting> Get(string gameExternalId, string teamExternalId)
        {
            var team = _context.Teams
                .Where(x => x.ExternalId == teamExternalId
                            && x.Game.ExternalId == gameExternalId)
                .Include(x => x.Sightings)
                .ThenInclude(y => y.Guard)
                .FirstOrDefault();

            return team?.Sightings ?? Enumerable.Empty<Sighting>();
        }
    }
}
