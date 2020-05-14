using System;
using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamSightingMapper _teamSightingMapper;
        private readonly ITeamCollectedItemMapper _teamCollectedItemMapper;
        private readonly ITeamCollectableItemMapper _teamCollectableItemMapper;

        public TeamService(
            IGameRepository gameRepository, 
            ITeamRepository teamRepository, 
            ITeamSightingMapper teamSightingMapper,
            ITeamCollectedItemMapper teamCollectedItemMapper,
            ITeamCollectableItemMapper teamCollectableItemMapper)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _teamSightingMapper = teamSightingMapper;
            _teamCollectedItemMapper = teamCollectedItemMapper;
            _teamCollectableItemMapper = teamCollectableItemMapper;
        }

        public async Task<bool> CanJoinTeamAsync(string gameExternalId, string teamExternalId)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);

            var team = game?.Teams.FirstOrDefault(x => x.ExternalId == teamExternalId);
            return team != null;
        }

        public async Task<TeamGameResponse> GetTeamGameAsync(string gameExternalId, string teamExternalId)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);
            if (game == null)
            {
                throw new InvalidOperationException($"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == false)
            {
                throw new InvalidOperationException($"No active game found with external Id: {gameExternalId}");
            }

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new InvalidOperationException($"No team found with external Id: {teamExternalId}");
            }

            team = await _teamRepository.Get(team.TeamId);
            var remainingItems = game.CollectableItems.Where(x =>
                !team.CollectedItems.Select(y => y.CollectableItem.CollectableItemId)
                    .Contains(x.CollectableItemId));

            var response = new TeamGameResponse
            {
                ExternalId = team.ExternalId,
                Name = team.Name,
                Sightings = _teamSightingMapper.Map(team.Sightings),
                ItemsCollected = _teamCollectedItemMapper.Map(team.CollectedItems),
                // TODO does this fit on the team game?  Its part of the game 
                RemainingItems = _teamCollectableItemMapper.Map(remainingItems)
            };

            return response;
        }
    }
}
