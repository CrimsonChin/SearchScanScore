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

        public async Task AddCollectedItemAsync(string gameCode, string teamCode, string itemCode)
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

            var team = game.Teams.SingleOrDefault(x => x.Code == teamCode);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No team found with code: {teamCode}");
            }

            var item = game.CollectableItems.SingleOrDefault(x => x.Code == itemCode);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No item found with code: {itemCode}");
            }

            var collectedItems = await _collectedItemRepository.GetCollectedItemsAsync(gameCode, teamCode);
            if (collectedItems.Any(x => x.CollectableItem.Code == itemCode))
            {
                throw new HttpResponseException(HttpStatusCode.Conflict, $"Item {itemCode} already collected");
            }

            _collectedItemRepository.Add(new Entities.CollectedItem
            {
                ExternalId = Guid.NewGuid(),
                Team = team,
                CollectableItem = item,
                CollectedAt = DateTime.UtcNow
            });

            await _collectedItemRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CollectedItemResponse>> GetCollectedItemsAsync(string gameCode, string teamCode)
        {
            var game = await _gameRepository.GetAsync(gameCode);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No game found with code: {gameCode}");
            }

            var team = game.Teams.SingleOrDefault(x => x.Code == teamCode);
            if (team == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No team found with code: {teamCode}");
            }

            var collectedItems = await _collectedItemRepository.GetCollectedItemsAsync(gameCode, teamCode);

            return _collectedItemMapper.Map(collectedItems);
        }
    }
}
