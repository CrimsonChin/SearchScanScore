using System.Threading.Tasks;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Team.Controllers
{
    [Area("Team")]
    [Route("api/team/[controller]")]
    [ApiController]
    public class SightingController : Controller
    {
        private readonly ISightingService _sightingService;

        public SightingController(ISightingService sightingService)
        {
            _sightingService = sightingService;
        }

        [HttpGet("Get/{gameExternalId}/{teamExternalId}")]
        public async Task<IActionResult> Get(string gameExternalId, string teamExternalId)
        {
            var sightings = await _sightingService.GetSightingsAsync(gameExternalId, teamExternalId);

            return Ok(sightings);
        }
    }
}
