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
    internal class CollectableItemService : ICollectableItemService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IAnonymousCollectableItemMapper _anonymousCollectableItemMapper;

        public CollectableItemService(IGameRepository gameRepository, ITeamRepository teamRepository, IAnonymousCollectableItemMapper anonymousCollectableItemMapper)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _anonymousCollectableItemMapper = anonymousCollectableItemMapper;
        }

        public async Task<IEnumerable<AnonymousCollectableItemResponse>> GetAnonymousAsync(string gameExternalId)
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

            var response = _anonymousCollectableItemMapper.Map(game.CollectableItems);

            return response;
        }

        public async Task<IEnumerable<AnonymousCollectableItemResponse>> GetRemainingAsync(string gameExternalId, string teamExternalId)
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

            team = await _teamRepository.Get(team.TeamId);
            var remainingItems = game.CollectableItems.Where(x =>
                !team.CollectedItems.Select(y => y.CollectableItem.CollectableItemId)
                    .Contains(x.CollectableItemId));

            var response = _anonymousCollectableItemMapper.Map(remainingItems);

            return response;
        }
    }
}
