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

        [HttpGet("Join/{gameCode}/{teamCode}")]
        public async Task<IActionResult> Join(string gameCode, string teamCode)
        {
            var canJoinTeam = await _teamService.CanJoinTeamAsync(gameCode, teamCode);

            return Ok(canJoinTeam);
        }
    }
}
