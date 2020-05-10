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

        [HttpPost("Start/{gameExternalId}")]
        public async Task Start(string gameExternalId)
        {
            await _gameService.StartGame(gameExternalId);
        }

        [HttpPost("Stop/{gameExternalId}")]
        public async Task Stop(string gameExternalId)
        {
            await _gameService.StopGame(gameExternalId);
        }

        [HttpPost("Reset/{gameExternalId}")]
        public async Task Reset(string gameExternalId)
        {
            await _gameService.Reset(gameExternalId);
        }

        [HttpGet("Get/{gameExternalId}")]
        public ActionResult Get(string gameExternalId)
        {
            var game = _gameService.Get(gameExternalId);
            if (game == null)
            {
                return NotFound($"No Game with external id {gameExternalId} found");
            }

            return Ok(game);
        }
    }
}
