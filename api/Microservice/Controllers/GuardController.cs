using Microsoft.AspNetCore.Mvc;
using SearchScanScore.Services.Interfaces;

namespace Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardController : ControllerBase
    {
        private readonly IGuardService _guardService;

        public GuardController(IGuardService guardService)
        {
            _guardService = guardService;
        }

        [HttpPost("RecordSighting/{gameExternalId}/{guardExternalId}/{teamExternalId}")]
        public ActionResult<bool> RecordSighting(string gameExternalId, string guardExternalId, string teamExternalId)
        {
            _guardService.RecordSighting(gameExternalId, guardExternalId, teamExternalId);
            
            return true;
        }
    }
}
