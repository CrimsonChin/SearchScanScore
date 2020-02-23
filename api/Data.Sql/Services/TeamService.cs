using System;
using System.Collections.Generic;
using System.Linq;
using Data.Sql.Data;
using Microsoft.EntityFrameworkCore;
using SearchScanScore.Services.Interfaces;

namespace Data.Sql.Services
{
    public class TeamService : ITeamService
    {
        private readonly SearchScanScoreContext _context;

        public TeamService(SearchScanScoreContext context)
        {
            _context = context;
        }

        void ITeamService.CollectItem(string gameExternalId, string teamExternalId, string itemExternalId)
        {
            var activeGame = _context.Games
                .Include(game => game.Teams)
                .ThenInclude(t => t.CollectedItems)
                .Include(game => game.CollectableItems)
                .SingleOrDefault(game => game.ExternalId == gameExternalId
                                         && game.IsActive);

            if (activeGame == null)
            {
                throw new ArgumentNullException($"No active game with {gameExternalId} found");
            }

            var team = activeGame.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new ArgumentNullException("No team found");
            }

            var item = activeGame.CollectableItems.SingleOrDefault(x => x.ExternalId == itemExternalId);
            if (item == null)
            {
                throw new ArgumentNullException("No item found");
            }

            if (team.CollectedItems.Any(x => x.CollectableItem.ExternalId == itemExternalId))
            {
                throw new ArgumentNullException("Item already collected");
            }

            _context.CollectedItems.Add(new Entities.CollectedItem
            {
                Team = team,
                CollectableItem = item,
                CollectedAt = DateTime.UtcNow
            });

            _context.SaveChanges();
        }

        int ITeamService.GetTeamScore(string gameExternalId, string teamExternalId)
        {
            var team = _context.Teams
                .Where(x => x.ExternalId == teamExternalId
                            && x.Game.ExternalId == gameExternalId
                            && x.Game.IsActive)
                .Include(x => x.CollectedItems)
                .FirstOrDefault();

            return team?.CollectedItems?.Count ?? 0;
        }

        IList<SearchScanScore.Services.Models.CollectedItem> ITeamService.GetCollectedItems(string gameExternalId, string teamExternalId)
        {
            var team = _context.Teams
                .Where(x => x.ExternalId == teamExternalId
                            && x.Game.ExternalId == gameExternalId
                            && x.Game.IsActive)
                .Include(x => x.CollectedItems)
                .ThenInclude(y => y.CollectableItem)
                .FirstOrDefault();

            return team?.CollectedItems
                       .Select(x => new SearchScanScore.Services.Models.CollectedItem
                       {
                           TeamId = x.Team.ExternalId,
                           TeamName = x.Team.Name,
                           CollectableItemId = x.CollectableItem.ExternalId,
                           CollectableItemName = x.CollectableItem.Name,
                           CollectedAt = x.CollectedAt
                       }).ToList() ?? new List<SearchScanScore.Services.Models.CollectedItem>();
        }
    }
}
