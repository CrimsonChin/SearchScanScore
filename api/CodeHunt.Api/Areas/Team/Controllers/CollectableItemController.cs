using System.Threading.Tasks;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Team.Controllers
{
    [Area("Team")]
    [Route("api/team/[controller]")]
    [ApiController]
    public class CollectableItemController : Controller
    {
        private readonly ICollectableItemService _collectableItemService;

        public CollectableItemController(ICollectableItemService collectableItemService)
        {
            _collectableItemService = collectableItemService;
        }

        [HttpGet("Get/{gameExternalId}")]
        public async Task<IActionResult> Get(string gameExternalId)
        {
            var response = await _collectableItemService.GetAnonymousAsync(gameExternalId);

            return Ok(response);
        }

        [HttpGet("GetRemaining/{gameExternalId}/{teamExternalId}")]
        public async Task<IActionResult> GetRemaining(string gameExternalId, string teamExternalId)
        {
            var collectableItems = await _collectableItemService.GetRemainingAsync(gameExternalId, teamExternalId);

            return Ok(collectableItems);
        }
    }
}
