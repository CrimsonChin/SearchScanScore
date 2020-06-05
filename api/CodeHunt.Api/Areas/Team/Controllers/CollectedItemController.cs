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

        [HttpPost("Add/{gameExternalId}/{teamExternalId}/{collectableItemExternalId}")]
        public async Task<IActionResult> Add(string gameExternalId, string teamExternalId, string collectableItemExternalId)
        {
            await _collectedItemService.AddCollectedItemAsync(gameExternalId, teamExternalId, collectableItemExternalId);
            await _teamNotificationService.SendItemFoundNotificationAsync(gameExternalId, teamExternalId, collectableItemExternalId);

            return Ok(true);
        }

        [HttpGet("Get/{gameExternalId}/{teamExternalId}")]
        public async Task<IActionResult> Get(string gameExternalId, string teamExternalId)
        {
            var collectedItems = await _collectedItemService.GetCollectedItemsAsync(gameExternalId, teamExternalId);

            return Ok(collectedItems);
        }
    }
}
