using System.Net;
using System.Threading.Tasks;
using CodeHunt.Domain.Exceptions;
using CodeHunt.Domain.Mappers;
using CodeHunt.Domain.Repositories;
using CodeHunt.Domain.Responses;

namespace CodeHunt.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGameMapper _gameMapper;

        public GameService(IGameRepository gameRepository, IGameMapper gameMapper)
        {
            _gameRepository = gameRepository;
            _gameMapper = gameMapper;
        }

        public async Task<GameResponse> GetAsync(string gameCode)
        {
            var game = await _gameRepository.GetAsync(gameCode);

            return _gameMapper.Map(game);
        }

        public async Task StartGameAsync(string gameCode)
        {
            await SetIsActiveStateAsync(gameCode, true);
        }

        public async Task StopGameAsync(string gameCode)
        {
            await SetIsActiveStateAsync(gameCode, false);
        }

        public async Task ResetAsync(string gameCode)
        {
            _gameRepository.Reset(gameCode);
            await _gameRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task SetIsActiveStateAsync(string gameExternalId, bool isActive)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"No game found with code: {gameExternalId}");
            }

            if (game.IsActive == isActive)
            {
                return;
            }

            game.IsActive = isActive;
            
            _gameRepository.Update(game);

            await _gameRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
