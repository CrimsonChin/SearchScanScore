using System.Threading.Tasks;
using CodeHunt.Api.NotificationServices;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ITeamNotificationService _teamNotificationService;
        private readonly ICollectedItemService _collectedItemService;

        public TeamController(ITeamService teamService, ITeamNotificationService teamNotificationService, ICollectedItemService collectedItemService)
        {
            _teamService = teamService;
            _teamNotificationService = teamNotificationService;
            _collectedItemService = collectedItemService;
        }

        [HttpGet("CanJoin/{gameExternalId}/{teamExternalId}")]
        public async Task<IActionResult> CanJoinTeam(string gameExternalId, string teamExternalId)
        {
            var canJoinTeam = await _teamService.CanJoinTeamAsync(gameExternalId, teamExternalId);

            return Ok(canJoinTeam);
        }

        // TODO move to collectedItem Controller
        [HttpPost("AddCollectedItem/{gameExternalId}/{teamExternalId}/{collectableItemExternalId}")]
        public async Task<IActionResult> AddCollectedItem(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            await _collectedItemService.AddCollectedItemAsync(gameExternalId, teamExternalId, collectableItemExternalId);
            await _teamNotificationService.SendItemFoundNotificationAsync(gameExternalId, teamExternalId, collectableItemExternalId);

            return Ok(true);
        }

        [HttpGet("GetAsync/{gameExternalId}/{teamExternalId}")]
        public async Task<IActionResult> Get(string gameExternalId, string teamExternalId)
        {
            var response = await _teamService.GetTeamGameAsync(gameExternalId, teamExternalId);

            return Ok(response);
        }
    }
}
