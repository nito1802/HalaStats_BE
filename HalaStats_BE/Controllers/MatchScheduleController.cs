using HalaStats_BE.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HalaStats_BE.Controllers
{
    [ApiController]
    [EnableCors("AllowCors"), Route("[controller]")]
    public class MatchScheduleController : Controller
    {
        private readonly IPlayerService _playerService;

        public MatchScheduleController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<IActionResult> SeedPlayers()
        {
            await _playerService.SeedPlayers();
            return Ok();
        }
    }
}