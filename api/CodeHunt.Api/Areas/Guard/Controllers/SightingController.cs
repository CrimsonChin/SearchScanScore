using System.Threading.Tasks;
using CodeHunt.Api.NotificationServices;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Guard.Controllers
{
    [Area("Guard")]
    [Route("api/guard/[controller]")]
    [ApiController]
    public class SightingController : Controller
    {
        private readonly ISightingService _sightingService;
        private readonly ITeamNotificationService _teamNotificationService;

        public SightingController(ISightingService sightingService, ITeamNotificationService teamNotificationService)
        {
            _sightingService = sightingService;
            _teamNotificationService = teamNotificationService;
        }

        [HttpPost("Add/{gameExternalId}/{guardExternalId}/{teamExternalId}")]
        public async Task<IActionResult> Add(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            await _sightingService.AddSightingAsync(gameExternalId, guardExternalId, teamExternalId);
            await _teamNotificationService.SendSightedNotificationAsync(gameExternalId, teamExternalId, guardExternalId);

            return Ok(true);
        }
    }
}
