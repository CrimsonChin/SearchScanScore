using System.Threading.Tasks;
using CodeHunt.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPut("Start/{gameExternalId}")]
        public async Task Start(string gameExternalId)
        {
            await _gameService.StartGameAsync(gameExternalId);
        }

        [HttpPut("Stop/{gameExternalId}")]
        public async Task Stop(string gameExternalId)
        {
            await _gameService.StopGameAsync(gameExternalId);
        }

        [HttpPost("Reset/{gameExternalId}")]
        public async Task Reset(string gameExternalId)
        {
            await _gameService.ResetAsync(gameExternalId);
        }

        [HttpGet("Get/{gameExternalId}")]
        public async Task<ActionResult> Get(string gameExternalId)
        {
            var game = await _gameService.GetAsync(gameExternalId);
            if (game == null)
            {
                return NotFound($"No GameResponse with external id {gameExternalId} found");
            }

            return Ok(game);
        }
    }
}
