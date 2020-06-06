using System.Threading.Tasks;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPut("Start/{gameCode}")]
        public async Task Start(string gameCode)
        {
            await _gameService.StartGameAsync(gameCode);
        }

        [HttpPut("Stop/{gameCode}")]
        public async Task Stop(string gameCode)
        {
            await _gameService.StopGameAsync(gameCode);
        }

        [HttpPost("Reset/{gameCode}")]
        public async Task Reset(string gameCode)
        {
            await _gameService.ResetAsync(gameCode);
        }

        [HttpGet("Get/{gameCode}")]
        public async Task<ActionResult> Get(string gameCode)
        {
            var game = await _gameService.GetAsync(gameCode);
            if (game == null)
            {
                return NotFound($"No game found with code: {gameCode}");
            }

            return Ok(game);
        }
    }
}
