using System.Threading.Tasks;
using CodeHunt.Api.NotificationServices;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardController : ControllerBase
    {
        private readonly IGuardService _guardService;
        private readonly ITeamNotificationService _teamNotificationService;

        public GuardController(IGuardService guardService, ITeamNotificationService teamNotificationService)
        {
            _guardService = guardService;
            _teamNotificationService = teamNotificationService;
        }

        // TODO CanJoin

        [HttpPost("AddSighting/{gameExternalId}/{guardExternalId}/{teamExternalId}")]
        public async Task<ActionResult<bool>> AddSighting(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            await _guardService.AddSighting(gameExternalId, guardExternalId, teamExternalId);
            await _teamNotificationService.SendSightedNotification(gameExternalId, teamExternalId, guardExternalId);

            return Ok(true);
        }
    }
}
