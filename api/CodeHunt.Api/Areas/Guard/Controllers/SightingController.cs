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

        [HttpPost("Add/{gameCode}/{guardCode}/{teamCode}")]
        public async Task<IActionResult> Add(string gameCode, string guardCode, string teamCode)
        {
            await _sightingService.AddSightingAsync(gameCode, guardCode, teamCode);
            await _teamNotificationService.SendSightedNotificationAsync(gameCode, teamCode, guardCode);

            return Ok(true);
        }
    }
}
