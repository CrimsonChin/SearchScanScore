using Microservice.NotificationServices;
using Microsoft.AspNetCore.Mvc;
using SearchScanScore.Services.Interfaces;

namespace Microservice.Controllers
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

        [HttpPost("RecordSighting/{gameExternalId}/{guardExternalId}/{teamExternalId}")]
        public ActionResult<bool> RecordSighting(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            _guardService.RecordSighting(gameExternalId, guardExternalId, teamExternalId);

            _teamNotificationService.SendSightedNotification(gameExternalId, teamExternalId, guardExternalId);

            return true;
        }
    }
}
