using Microsoft.AspNetCore.Mvc;

namespace CodeHunt.Api.Areas.Guard.Controllers
{
    [Area("Guard")]
    [Route("api/guard/[controller]")]
    [ApiController]
    public class GuardController : ControllerBase
    {
        [HttpGet("Join/{gameCode}/{guardCode}")]
        public IActionResult Join(string gameCode, string guardCode)
        {
            return Ok(true);
        }
    }
}
