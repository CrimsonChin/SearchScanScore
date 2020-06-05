using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CodeHunt.Domain.Exceptions;
using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public class CollectedItemService : ICollectedItemService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICollectedItemRepository _collectedItemRepository;
        private readonly ICollectedItemMapper _collectedItemMapper;

        public CollectedItemService(
            IGameRepository gameRepository, 
            ICollectedItemRepository collectedItemRepository, 
            ICollectedItemMapper collectedItemMapper)
        {
            _gameRepository = gameRepository;
            _collectedItemRepository = collectedItemRepository;
            _collectedItemMapper = collectedItemMapper;
        }

        public async Task AddCollectedItemAsync(string gameExternalId, string teamExternalId, string itemExternalId)
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

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No team found with external Id: {teamExternalId}");
            }

            var item = game.CollectableItems.SingleOrDefault(x => x.ExternalId == itemExternalId);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No item found with external Id: {itemExternalId}");
            }

            var collectedItems = await _collectedItemRepository.GetCollectedItemsAsync(gameExternalId, teamExternalId);
            if (collectedItems.Any(x => x.CollectableItem.ExternalId == itemExternalId))
            {
                throw new HttpResponseException(HttpStatusCode.Conflict, $"Item {itemExternalId} already collected");
            }

            _collectedItemRepository.Add(new Entities.CollectedItem
            {
                Team = team,
                CollectableItem = item,
                CollectedAt = DateTime.UtcNow
            });

            await _collectedItemRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CollectedItemResponse>> GetCollectedItemsAsync(string gameExternalId, string teamExternalId)
        {
            // TODO validation

            var collectedItems = await _collectedItemRepository.GetCollectedItemsAsync(gameExternalId, teamExternalId);

            return _collectedItemMapper.Map(collectedItems);
        }
    }
}
