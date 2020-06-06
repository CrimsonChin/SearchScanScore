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

        [HttpGet("Get/{gameCode}/{teamCode}")]
        public async Task<IActionResult> Get(string gameCode, string teamCode)
        {
            var sightings = await _sightingService.GetSightingsAsync(gameCode, teamCode);

            return Ok(sightings);
        }
    }
}
