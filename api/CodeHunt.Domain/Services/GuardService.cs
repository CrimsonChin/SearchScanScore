using System;
using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Repositories;

namespace CodeHunt.Domain.Services
{
    public class GuardService : IGuardService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISightingRepository _sightingRepository;

        public GuardService(IGameRepository gameRepository, ISightingRepository sightingRepository)
        {
            _gameRepository = gameRepository;
            _sightingRepository = sightingRepository;
        }
        
        public async Task AddSighting(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            var game = _gameRepository.Get(gameExternalId);
            if (game == null)
            {
                throw new InvalidOperationException($"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == false)
            {
                throw new InvalidOperationException($"No active game found with external Id: {gameExternalId}");
            }

            var guard = game.Guards.SingleOrDefault(x => x.ExternalId == guardExternalId);
            if (guard == null)
            {
                throw new InvalidOperationException($"No guard found with external Id: {gameExternalId}");
            }

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new InvalidOperationException($"No team found with external Id: {teamExternalId}");
            }

            _sightingRepository.Add(new Sighting { Team = team, Guard = guard, SightedAt = DateTime.UtcNow });

            await _sightingRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
