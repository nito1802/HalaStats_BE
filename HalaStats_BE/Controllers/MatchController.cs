using HalaStats_BE.Dtos.Requests;
using HalaStats_BE.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HalaStats_BE.Controllers
{
    [ApiController]
    [EnableCors("AllowCors"), Route("[controller]")]
    public class MatchController : Controller
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost]
        [Route("calculate-ratings")]
        public async Task<IActionResult> CalculatePlayersRatings(MatchResultDto matchResult)
        {
            var serialized = JsonConvert.SerializeObject(matchResult, Formatting.Indented);
            await _matchService.CalculatePlayersRatings(matchResult);
            return Ok();
        }

        [HttpGet]
        [Route("matches-history")]
        public async Task<IActionResult> GetMatchesHistory()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var result = await _matchService.GetMatchesHistory();
            sw.Stop();
            return Ok(result);
        }
    }
}