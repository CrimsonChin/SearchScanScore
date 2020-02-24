using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchScanScore.Services.Interfaces;

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
        public bool CollectItem(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            _teamService.CollectItem(gameExternalId, teamExternalId, collectableItemExternalId);

            return true;
        }

        [HttpGet("Get/{gameExternalId}/{teamExternalId}")]
        public string Get(string gameExternalId, string teamExternalId)
        {
            var game = _gameService.Get(gameExternalId);
            var team = game.Teams.FirstOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                return null;
            }

            var collectedItems = _teamService.GetCollectedItems(gameExternalId, teamExternalId);
            var sightings = _teamService.GetSightings(gameExternalId, teamExternalId);

            return JsonConvert.SerializeObject(new
            {
                team.ExternalId,
                team.Name,
                Sightings = sightings.Select(x => new
                {
                    SightedAt = x.SightedAt.ToString("g"),
                    x.SightedBy
                }),
                TotalItemsCollected = collectedItems.Count,
                ItemsCollected = collectedItems.Select(x =>
                    new
                    {
                        ExternalId = x.CollectableItemId,
                        Name = x.CollectableItemName,
                        CollectedAt = x.CollectedAt.ToString("g")
                    }).ToList()
            });
        }
    }
}
