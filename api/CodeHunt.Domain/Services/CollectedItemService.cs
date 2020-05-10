using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public class CollectedItemService : ICollectedItemService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICollectedItemRepository _collectedItemRepository;
        private readonly ITeamCollectedItemMapper _teamCollectedItemMapper;

        public CollectedItemService(
            IGameRepository gameRepository, 
            ICollectedItemRepository collectedItemRepository, 
            ITeamCollectedItemMapper teamCollectedItemMapper)
        {
            _gameRepository = gameRepository;
            _collectedItemRepository = collectedItemRepository;
            _teamCollectedItemMapper = teamCollectedItemMapper;
        }

        public async Task AddCollectedItem(string gameExternalId, string teamExternalId, string itemExternalId)
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

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new InvalidOperationException($"No team found with external Id: {teamExternalId}");
            }

            var item = game.CollectableItems.SingleOrDefault(x => x.ExternalId == itemExternalId);
            if (item == null)
            {
                throw new InvalidOperationException($"No item found with external Id: {itemExternalId}");
            }

            // Another db call?
            var collectedItems = _collectedItemRepository.GetCollectedItems(gameExternalId, teamExternalId);
            if (collectedItems.Any(x => x.CollectableItem.ExternalId == itemExternalId))
            {
                throw new InvalidOperationException($"Item {itemExternalId} already collected");
            }

            _collectedItemRepository.Add(new Entities.CollectedItem
            {
                Team = team,
                CollectableItem = item,
                CollectedAt = DateTime.UtcNow
            });

            await _collectedItemRepository.UnitOfWork.SaveChangesAsync();
        }

        public IEnumerable<TeamCollectedItemResponse> GetCollectedItems(string gameExternalId, string teamExternalId)
        {
            // TODO validation

            var collectedItems = _collectedItemRepository.GetCollectedItems(gameExternalId, teamExternalId);

            return _teamCollectedItemMapper.Map(collectedItems);
        }
    }
}
