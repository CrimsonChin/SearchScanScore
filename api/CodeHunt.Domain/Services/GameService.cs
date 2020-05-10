using System;
using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Models;
using CodeHunt.Domain.Repositories;

namespace CodeHunt.Domain.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Game Get(string gameExternalId)
        {
            var game = _gameRepository.Get(gameExternalId);

            return new Game
            {
                ExternalId = gameExternalId,
                Name = game.Name,
                IsActive = game.IsActive,
                Teams = game.Teams.Select(x => new Team
                {
                    ExternalId = x.ExternalId,
                    Name = x.Name
                }),
                Guards = game.Guards.Select(x => new Guard
                {
                    ExternalId = x.ExternalId,
                    Name = x.Name
                }),
                CollectableItems = game.CollectableItems.Select(x => new CollectableItem
                {
                    ExternalId = x.ExternalId,
                    Name = x.Name
                })
            };
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
