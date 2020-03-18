using System;
using System.Linq;
using Data.Sql.Data;
using Microsoft.EntityFrameworkCore;
using SearchScanScore.Services.Interfaces;
using SearchScanScore.Services.Models;

namespace Data.Sql.Services
{
    public class GameService : IGameService
    {
        private readonly SearchScanScoreContext _context;

        public GameService(SearchScanScoreContext context)
        {
            _context = context;
        }

        public Game Get(string gameExternalId)
        {
            var game = _context.Games
                .Include(x => x.Teams)
                .Include(x => x.Guards)
                .Include(x => x.CollectableItems)
                .SingleOrDefault(x => x.ExternalId == gameExternalId);

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

        void IGameService.StartGame(string gameExternalId)
        {
            SetIsActiveState(gameExternalId, true);
        }

        void IGameService.StopGame(string gameExternalId)
        {
            SetIsActiveState(gameExternalId, false);
        }

        void IGameService.ResetCollectedItems(string gameExternalId)
        {
            var game = _context.Games
                .Include(x => x.Teams)
                .ThenInclude(x => x.CollectedItems)
                .SingleOrDefault(x => x.ExternalId == gameExternalId);
            
            if (game == null)
            {
                throw new ArgumentNullException($"No game with {gameExternalId} found");
            }

            foreach (var team in game.Teams)
            {
                _context.CollectedItems.RemoveRange(team.CollectedItems);
            }

            _context.SaveChanges();
        }

        private void SetIsActiveState(string gameExternalId, bool isActive)
        {
            var game = GetGame(gameExternalId);
            if (game == null)
            {
                throw new ArgumentNullException($"No game with {gameExternalId} found");
            }

            if (game.IsActive == isActive)
            {
                var message = isActive
                    ? "Game is already active"
                    : "Game is not currently active";

                throw new InvalidOperationException(message);
            }

            game.IsActive = isActive;

            _context.SaveChanges();
        }

        private Entities.Game GetGame(string gameExternalId)
        {
            var game = _context.Games.SingleOrDefault(x => x.ExternalId == gameExternalId);
            if (game == null)
            {
                throw new ArgumentNullException($"No game with {gameExternalId} found");
            }

            return game;
        }
    }
}
