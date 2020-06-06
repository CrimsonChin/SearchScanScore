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
        
        public async Task AddSightingAsync(string gameCode, string guardCode, string teamCode)
        {
            var game = await _gameRepository.GetAsync(gameCode);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No game found with code: {gameCode}");
            }

            if (game.IsActive == false)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No active game found with code: {gameCode}");
            }

            var guard = game.Guards.SingleOrDefault(x => x.Code == guardCode);
            if (guard == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No guard found with code: {gameCode}");
            }

            var team = game.Teams.SingleOrDefault(x => x.Code == teamCode);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No team found with code: {teamCode}");
            }

            _sightingRepository.Add(new Sighting
            {
                ExternalId = Guid.NewGuid(),
                Team = team,
                Guard = guard,
                SightedAt = DateTime.UtcNow
            });

            await _sightingRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<SightingResponse>> GetSightingsAsync(string gameCode, string teamCode)
        {
            var sightings = await _sightingRepository.GetAsync(gameCode, teamCode);

            return _teamSightingMapper.Map(sightings);
        }
    }
}
