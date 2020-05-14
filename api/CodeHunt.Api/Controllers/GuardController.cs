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
        private readonly ISightingService _sightingService;
        private readonly ITeamNotificationService _teamNotificationService;

        public GuardController(ISightingService sightingService, ITeamNotificationService teamNotificationService)
        {
            _sightingService = sightingService;
            _teamNotificationService = teamNotificationService;
        }

        // TODO CanJoin

        // TODO move to Sighting Controller
        [HttpPost("AddSighting/{gameExternalId}/{guardExternalId}/{teamExternalId}")]
        public async Task<ActionResult<bool>> AddSighting(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            await _sightingService.AddSightingAsync(gameExternalId, guardExternalId, teamExternalId);
            await _teamNotificationService.SendSightedNotificationAsync(gameExternalId, teamExternalId, guardExternalId);

            return Ok(true);
        }
    }
}
