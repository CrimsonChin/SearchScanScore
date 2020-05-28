using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CodeHunt.Domain.Entities;
using CodeHunt.Domain.Exceptions;
using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public class SightingService : ISightingService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ISightingRepository _sightingRepository;
        private readonly ITeamSightingMapper _teamSightingMapper;

        public SightingService(IGameRepository gameRepository, ISightingRepository sightingRepository, ITeamSightingMapper teamSightingMapper)
        {
            _gameRepository = gameRepository;
            _sightingRepository = sightingRepository;
            _teamSightingMapper = teamSightingMapper;
        }
        
        public async Task AddSightingAsync(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == false)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No active game found with external Id: {gameExternalId}");
            }

            var guard = game.Guards.SingleOrDefault(x => x.ExternalId == guardExternalId);
            if (guard == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No guard found with external Id: {gameExternalId}");
            }

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No team found with external Id: {teamExternalId}");
            }

            _sightingRepository.Add(new Sighting { Team = team, Guard = guard, SightedAt = DateTime.UtcNow });

            await _sightingRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task<IEnumerable<TeamSightingResponse>> GetSightingsAsync(string gameExternalId, string teamExternalId)
        {
            var sightings = await _sightingRepository.GetAsync(gameExternalId, teamExternalId);

            return _teamSightingMapper.Map(sightings);
        }
    }
}
