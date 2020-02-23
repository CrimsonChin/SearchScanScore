using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchScanScore.Services.Interfaces;

namespace Microservice.Controllers
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

        [HttpPost("StartGame/{gameExternalId}")]
        public void StartGame(string gameExternalId)
        {
            _gameService.StartGame(gameExternalId);
        }

        [HttpPost("StopGame/{gameExternalId}")]
        public void StopGame(string gameExternalId)
        {
            _gameService.StopGame(gameExternalId);
        }

        [HttpPost("ResetCollectedItems/{gameExternalId}")]
        public void ResetCollectedItems(string gameExternalId)
        {
            _gameService.ResetCollectedItems(gameExternalId);
        }
        
        [HttpGet("Get/{gameExternalId}")]
        public ActionResult<string> Get(string gameExternalId)
        {
            var game = _gameService.Get(gameExternalId);

            return JsonConvert.SerializeObject(game);
        }
    }
}
