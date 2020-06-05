using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Guard.Controllers
{
    [Area("Guard")]
    [Route("api/guard/[controller]")]
    [ApiController]
    public class GuardController : ControllerBase
    {
        [HttpGet("Join/{gameExternalId}/{guardExternalId}")]
        public IActionResult Join(string gameExternalId, string guardExternalId)
        {
            return Ok(true);
        }
    }
}
