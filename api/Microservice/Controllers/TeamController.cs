using System;
using System.Collections.Generic;
using System.Linq;
using Data.Sql.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchScanScore.Services.Interfaces;
using CollectableItem = SearchScanScore.Services.Models.CollectableItem;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ITeamService _teamService;

        public TeamController(IGameService gameService, ITeamService teamService)
        {
            _gameService = gameService;
            _teamService = teamService;
        }

        [HttpGet("CanJoinTeam/{gameExternalId}/{teamExternalId}")]
        public ActionResult<bool> CanJoinTeam(string gameExternalId, string teamExternalId)
        {
            var game = _gameService.Get(gameExternalId);

            return game.IsActive && game.Teams.Select(x => x.ExternalId).Contains(teamExternalId);
        }

        [HttpPost("CollectItem/{gameExternalId}/{teamExternalId}/{collectableItemExternalId}")]
        public string CollectItem(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            _teamService.CollectItem(gameExternalId, teamExternalId, collectableItemExternalId);

            // TODO response should Include whether the collection was a success or not
            return GetCollectedItems2(gameExternalId, teamExternalId);
        }

        [HttpGet("GetCollectedItems/{gameExternalId}/{teamExternalId}")]
        public string GetCollectedItems(string gameExternalId, string teamExternalId)
        {
            return GetCollectedItems2(gameExternalId, teamExternalId);
        }

        private string GetCollectedItems2(string gameExternalId, string teamExternalId)
        {
            var game = _gameService.Get(gameExternalId);
            var team = game.Teams.FirstOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                return null;
            }

            var collectedItems = _teamService.GetCollectedItems(gameExternalId, teamExternalId);

            var remainingCollectableItems = game.CollectableItems.Where(x =>
                !collectedItems
                    .Select(y => y.CollectableItemId)
                    .Contains(x.ExternalId)).ToList();

            return JsonConvert.SerializeObject(new
            {
                team.ExternalId,
                team.Name,
                TotalItemsCollected = collectedItems.Count,
                ItemsCollected = collectedItems.Select(x =>
                    new
                    {
                        ExternalId = x.CollectableItemId,
                        Name = x.CollectableItemName,
                        CollectedAt = x.CollectedAt.ToString("g")
                    }).ToList(),
                RemainingCollectableItems = remainingCollectableItems ?? new List<CollectableItem>()
            });
        }
    }
}
