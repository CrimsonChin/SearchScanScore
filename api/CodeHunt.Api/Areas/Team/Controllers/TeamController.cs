using System.Threading.Tasks;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Team.Controllers
{
    [Area("Team")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("Join/{gameExternalId}/{teamExternalId}")]
        public async Task<IActionResult> Join(string gameExternalId, string teamExternalId)
        {
            var canJoinTeam = await _teamService.CanJoinTeamAsync(gameExternalId, teamExternalId);

            return Ok(canJoinTeam);
        }
    }
}
