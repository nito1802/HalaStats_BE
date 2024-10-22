using HalaStats_BE.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HalaStats_BE.Controllers
{
    [ApiController]
    [EnableCors("AllowCors"), Route("[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        [Route("seed")]
        public async Task<IActionResult> SeedPlayers()
        {
            await _playerService.SeedPlayers();
            return Ok();
        }

        [HttpGet]
        [Route("rank")]
        public async Task<IActionResult> GetPlayersRank()
        {
            var result = await _playerService.GetPlayersRank();
            return Ok(result);
        }
    }
}