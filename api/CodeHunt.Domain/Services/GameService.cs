using System;
using System.Threading.Tasks;
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

        public async Task<GameResponse> GetAsync(string gameExternalId)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);

            return _gameMapper.Map(game);
        }

        public async Task StartGameAsync(string gameExternalId)
        {
            await SetIsActiveStateAsync(gameExternalId, true);
        }

        public async Task StopGameAsync(string gameExternalId)
        {
            await SetIsActiveStateAsync(gameExternalId, false);
        }

        public async Task ResetAsync(string gameExternalId)
        {
            _gameRepository.Reset(gameExternalId);
            await _gameRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task SetIsActiveStateAsync(string gameExternalId, bool isActive)
        {
            var game = await _gameRepository.GetAsync(gameExternalId);
            if (game == null)
            {
                throw new InvalidOperationException($"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == isActive)
            {
                var message = isActive
                    ? "Game is already started"
                    : "Game is already stopped";

                throw new InvalidOperationException(message);
            }

            game.IsActive = isActive;
            
            _gameRepository.Update(game);

            await _gameRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
