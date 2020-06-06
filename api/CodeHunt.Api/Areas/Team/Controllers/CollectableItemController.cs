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

        [HttpGet("Get/{gameCode}")]
        public async Task<IActionResult> Get(string gameCode)
        {
            var response = await _collectableItemService.GetAnonymousAsync(gameCode);

            return Ok(response);
        }

        //[HttpGet("GetRemaining/{gameCode}/{teamCode}")]
        //public async Task<IActionResult> GetRemaining(string gameCode, string teamCode)
        //{
        //    var collectableItems = await _collectableItemService.GetRemainingAsync(gameCode, teamCode);

        //    return Ok(collectableItems);
        //}
    }
}
