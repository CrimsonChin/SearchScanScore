using System.Linq;
using Microservice.NotificationServices;
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
        private readonly ITeamNotificationService _teamNotificationService;

        public TeamController(IGameService gameService, ITeamService teamService, ITeamNotificationService teamNotificationService)
        {
            _gameService = gameService;
            _teamService = teamService;
            _teamNotificationService = teamNotificationService;
        }

        [HttpGet("CanJoinTeam/{gameExternalId}/{teamExternalId}")]
        public ActionResult<bool> CanJoinTeam(string gameExternalId, string teamExternalId)
        {
            var game = _gameService.Get(gameExternalId);
            if (game == null)
            {
                return NotFound($"No Game with external id {gameExternalId} found");
            }

            return game.IsActive && game.Teams.Select(x => x.ExternalId).Contains(teamExternalId);
        }

        [HttpPost("CollectItem/{gameExternalId}/{teamExternalId}/{collectableItemExternalId}")]
        public bool CollectItem(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            _teamService.CollectItem(gameExternalId, teamExternalId, collectableItemExternalId);

            _teamNotificationService.SendItemFoundNotification(gameExternalId, teamExternalId, collectableItemExternalId);

            return true;
        }

        [HttpGet("Get/{gameExternalId}/{teamExternalId}")]
        public ActionResult<string> Get(string gameExternalId, string teamExternalId)
        {
            var game = _gameService.Get(gameExternalId);
            if (game == null)
            {
                return NotFound($"No Game with external id {gameExternalId} found");
            }

            var team = game.Teams.FirstOrDefault(x => x.ExternalId == teamExternalId);
            if (team == null)
            {
                return NotFound($"No team with external id {teamExternalId} found");
            }

            var collectedItems = _teamService.GetCollectedItems(gameExternalId, teamExternalId);
            var sightings = _teamService.GetSightings(gameExternalId, teamExternalId);

            return JsonConvert.SerializeObject(new
            {
                team.ExternalId,
                team.Name,
                Sightings = sightings.Select(sighting => new
                {
                    SightedAt = sighting.SightedAt.ToString("g"),
                    sighting.SightedBy
                }),
                TotalItemsCollected = collectedItems.Count,
                ItemsCollected = collectedItems.Select(collectedItem =>
                    new
                    {
                        Name = collectedItem.CollectableItemName,
                        CollectedAt = collectedItem.CollectedAt.ToString("g")
                    }),
                RemainingItems = game.CollectableItems.Where(x =>
                    !collectedItems
                        .Select(y => y.CollectableItemId)
                        .Contains(x.ExternalId)).Select(x => new
                {
                    x.Name
                })
            });
        }
    }
}
