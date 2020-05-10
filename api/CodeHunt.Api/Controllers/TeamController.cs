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

        public TeamController(ITeamService teamService, ITeamNotificationService teamNotificationService)
        {
            _teamService = teamService;
            _teamNotificationService = teamNotificationService;
        }

        [HttpGet("CanJoin/{gameExternalId}/{teamExternalId}")]
        public ActionResult CanJoinTeam(string gameExternalId, string teamExternalId)
        {
            var canJoinTeam = _teamService.CanJoinTeam(gameExternalId, teamExternalId);

            return Ok(canJoinTeam);
        }

        [HttpPost("AddCollectedItem/{gameExternalId}/{teamExternalId}/{collectableItemExternalId}")]
        public async Task<ActionResult> CollectItem(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            await _teamService.AddCollectedItem(gameExternalId, teamExternalId, collectableItemExternalId);
            await _teamNotificationService.SendItemFoundNotification(gameExternalId, teamExternalId, collectableItemExternalId);

            return Ok(true);
        }

        [HttpGet("Get/{gameExternalId}/{teamExternalId}")]
        public async Task<ActionResult> Get(string gameExternalId, string teamExternalId)
        {
            var teamStats = await _teamService.GetTeamStats(gameExternalId, teamExternalId);

            return Ok(teamStats);
        }
    }
}
