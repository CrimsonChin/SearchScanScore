using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHunt.Domain.Models;
using CodeHunt.Domain.Repositories;

namespace CodeHunt.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICollectedItemRepository _collectedItemRepository;
        private readonly ISightingRepository _sightingRepository;

        public TeamService(IGameRepository gameRepository, ITeamRepository teamRepository, ICollectedItemRepository collectedItemRepository,
            ISightingRepository sightingRepository)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _collectedItemRepository = collectedItemRepository;
            _sightingRepository = sightingRepository;
        }

        public bool CanJoinTeam(string gameExternalId, string teamExternalId)
        {
            var game = _gameRepository.Get(gameExternalId);

            var team = game?.Teams.FirstOrDefault(x => x.ExternalId == teamExternalId);
            return team != null;
        }

        public async Task AddCollectedItem(string gameExternalId, string teamExternalId, string itemExternalId)
        {
            var game = _gameRepository.Get(gameExternalId);
            if (game == null)
            {
                throw new InvalidOperationException($"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == false)
            {
                throw new InvalidOperationException($"No active game found with external Id: {gameExternalId}");
            }

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new InvalidOperationException($"No team found with external Id: {teamExternalId}");
            }

            var item = game.CollectableItems.SingleOrDefault(x => x.ExternalId == itemExternalId);
            if (item == null)
            {
                throw new InvalidOperationException($"No item found with external Id: {itemExternalId}");
            }

            // Another db call?
            var collectedItems = _collectedItemRepository.GetCollectedItems(gameExternalId, teamExternalId);
            if (collectedItems.Any(x => x.CollectableItem.ExternalId == itemExternalId))
            {
                throw new InvalidOperationException($"Item {itemExternalId} already collected");
            }

            _collectedItemRepository.Add(new Entities.CollectedItem
            {
                Team = team,
                CollectableItem = item,
                CollectedAt = DateTime.UtcNow
            });

            await _collectedItemRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<TeamStats> GetTeamStats(string gameExternalId, string teamExternalId)
        {
            var game = _gameRepository.Get(gameExternalId);
            if (game == null)
            {
                throw new InvalidOperationException($"No game found with external Id: {gameExternalId}");
            }

            if (game.IsActive == false)
            {
                throw new InvalidOperationException($"No active game found with external Id: {gameExternalId}");
            }

            var team = game.Teams.SingleOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                throw new InvalidOperationException($"No team found with external Id: {teamExternalId}");
            }

            team = await _teamRepository.Get(team.TeamId);

            var teamView = new TeamStats
            {
                ExternalId = teamExternalId,
                Name = team.Name,
                Sightings = team.Sightings.Select(entity => new Sighting
                {
                    SightedAt = entity.SightedAt,
                    SightedBy = entity.Guard.Name
                }).ToList(),
                TotalItemsCollected = team.CollectedItems.Count,
                ItemsCollected = team.CollectedItems.Select(entity => new CollectedItem
                {
                    CollectableItemExternalId = entity.CollectableItem.ExternalId,
                    CollectableItemName = entity.CollectableItem.Name,
                    CollectedAt = entity.CollectedAt
                }).ToList(),
                RemainingItems = game.CollectableItems.Where(x =>
                        !team.CollectedItems.Select(y => y.CollectableItem.CollectableItemId)
                            .Contains(x.CollectableItemId))
                    .Select(t => new CollectableItem
                    {
                        Name = t.Name
                    }).ToList()
            };

            return teamView;
        }

        private IList<CollectedItem> GetCollectedItems(string gameExternalId, string teamExternalId)
        {
            var collectedItems = _collectedItemRepository.GetCollectedItems(gameExternalId, teamExternalId);

            return collectedItems?.Select(entity => new CollectedItem
            {
                CollectableItemExternalId = entity.CollectableItem.ExternalId,
                CollectableItemName = entity.CollectableItem.Name,
                CollectedAt = entity.CollectedAt
            }).ToList();
        }

        private IEnumerable<Sighting> GetSightings(string gameExternalId, string teamExternalId)
        {
            var sightings = _sightingRepository.Get(gameExternalId, teamExternalId);

            return sightings?.Select(entity => new Sighting
            {
                SightedAt = entity.SightedAt,
                SightedBy = entity.Guard.Name
            }).ToList() ?? Enumerable.Empty<Sighting>();
        }
    }
}
