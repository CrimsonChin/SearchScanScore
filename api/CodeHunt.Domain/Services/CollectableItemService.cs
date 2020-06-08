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

        public async Task<IEnumerable<AnonymousCollectableItemResponse>> GetAnonymousAsync(string gameCode)
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

            var response = _anonymousCollectableItemMapper.Map(game.CollectableItems);

            return response;
        }
    }
}
