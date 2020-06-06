using System.Threading.Tasks;
using CodeHunt.Api.NotificationServices;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Team.Controllers
{
    [Area("Team")]
    [Route("api/team/[controller]")]
    [ApiController]
    public class CollectedItemController : ControllerBase
    {
        private readonly ITeamNotificationService _teamNotificationService;
        private readonly ICollectedItemService _collectedItemService;

        public CollectedItemController(ITeamNotificationService teamNotificationService, ICollectedItemService collectedItemService)
        {
            _teamNotificationService = teamNotificationService;
            _collectedItemService = collectedItemService;
        }

        [HttpPost("Add/{gameCode}/{teamCode}/{collectableItemCode}")]
        public async Task<IActionResult> Add(string gameCode, string teamCode, string collectableItemCode)
        {
            await _collectedItemService.AddCollectedItemAsync(gameCode, teamCode, collectableItemCode);
            await _teamNotificationService.SendItemFoundNotificationAsync(gameCode, teamCode, collectableItemCode);

            return Ok(true);
        }

        [HttpGet("Get/{gameCode}/{teamCode}")]
        public async Task<IActionResult> Get(string gameCode, string teamCode)
        {
            var collectedItems = await _collectedItemService.GetCollectedItemsAsync(gameCode, teamCode);

            return Ok(collectedItems);
        }
    }
}
