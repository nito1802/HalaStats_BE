using HalaStats_BE.Database;
using HalaStats_BE.Database.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HalaStats_BE.Controllers
{
    [ApiController]
    [EnableCors("AllowCors"), Route("[controller]")]
    public class SkarbnikController : Controller
    {
        private readonly IHalaStatsDbContext _halaStatsDbContext;

        public SkarbnikController(IHalaStatsDbContext halaStatsDbContext)
        {
            _halaStatsDbContext = halaStatsDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SeedPlayers()
        {
            List<PlayerEntity> playerEntities =
            [
                new()
                {
                    Id = "Damian Lis"
                },
                new()
                {
                    Id = "Marcin Raźny"
                },
            ];
            //TODO: dokonczyć

            throw new NotImplementedException();
        }
    }
}