using HalaStats_BE.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HalaStats_BE.Controllers
{
    [ApiController]
    [EnableCors("AllowCors"), Route("[controller]")]
    public class MatchScheduleController : Controller
    {
        private readonly IMatchScheduleService _matchScheduleService;

        public MatchScheduleController(IMatchScheduleService matchScheduleService)
        {
            _matchScheduleService = matchScheduleService;
        }

        [HttpPost]
        [Route("seed-schedule")]
        public async Task<IActionResult> SeedPlayers()
        {
            await _matchScheduleService.SeedSchedule();
            return Ok();
        }
    }
}