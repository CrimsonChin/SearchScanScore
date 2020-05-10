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

        public GameResponse Get(string gameExternalId)
        {
            var game = _gameRepository.Get(gameExternalId);

            return _gameMapper.Map(game);
        }

        public async Task StartGame(string gameExternalId)
        {
            await SetIsActiveState(gameExternalId, true);
        }

        public async Task StopGame(string gameExternalId)
        {
            await SetIsActiveState(gameExternalId, false);
        }

        public async Task Reset(string gameExternalId)
        {
            _gameRepository.Reset(gameExternalId);
            await _gameRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task SetIsActiveState(string gameExternalId, bool isActive)
        {
            var game = _gameRepository.Get(gameExternalId);
            
            if (game == null)
            {
                throw new InvalidOperationException($"No game with external Id {gameExternalId} found");
            }

            if (game.IsActive == isActive)
            {
                var message = isActive
                    ? "GameResponse is already started"
                    : "GameResponse is already stopped";

                throw new InvalidOperationException(message);
            }

            game.IsActive = isActive;
            
            _gameRepository.Update(game);

            await _gameRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
