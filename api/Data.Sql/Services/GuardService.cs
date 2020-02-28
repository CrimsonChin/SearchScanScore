using System;
using System.Linq;
using Data.Sql.Data;
using Microsoft.EntityFrameworkCore;
using SearchScanScore.Services.Interfaces;

namespace Data.Sql.Services
{
    public class GuardService : IGuardService
    {
        private readonly SearchScanScoreContext _context;

        public GuardService(SearchScanScoreContext context)
        {
            _context = context;
        }
        
        public void RecordSighting(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            var activeGame = _context.Games
                .Include(game => game.Teams)
                .Include(game => game.Guards)
                .SingleOrDefault(game => game.ExternalId == gameExternalId && game.IsActive);

            if (activeGame == null)
            {
                throw new ArgumentNullException($"No active game with {gameExternalId} found");
            }

            var guard = activeGame.Guards.SingleOrDefault(x => x.ExternalId == guardExternalId);
            if (guard == null)
            {
                throw new ArgumentNullException($"No guard found with external id {gameExternalId}");
            }

            var team = activeGame.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new ArgumentNullException($"No team found  with external id {teamExternalId}");
            }

            _context.Sightings.Add(new Entities.Sighting()
            {
                Team = team,
                Guard = guard,
                SightedAt = DateTime.UtcNow
            });

            _context.SaveChanges();
        }
    }
}
