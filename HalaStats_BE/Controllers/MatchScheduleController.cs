using HalaStats_BE.Dtos.Requests;
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

        [HttpPost]
        [Route("set-status")]
        public async Task<IActionResult> SetMatchStatus(MatchScheduleStatusDto matchScheduleStatusDto)
        {
            await _matchScheduleService.SetMatchStatus(matchScheduleStatusDto);
            return Ok();
        }

        [HttpGet]
        [Route("matches-schedule")]
        public async Task<IActionResult> GetMatchesSchedule()
        {
            var result = await _matchScheduleService.GetMatchesSchedule();
            return Ok(result);
        }

        [HttpGet]
        [Route("next-match")]
        public async Task<IActionResult> GetNextMatch()
        {
            var result = await _matchScheduleService.GetNextMatch();
            return Ok(result);
        }
    }
}