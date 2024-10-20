using HalaStats_BE.Dtos.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HalaStats_BE.Controllers
{
    [ApiController]
    [EnableCors("AllowCors"), Route("[controller]")]
    public class MatchController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CalculatePlayersRatings(MatchResultDto matchResult)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var result = await _englishTeacherService.GetTranslatedMp3(ocrImageDto);
            //    //return Ok(result);

            //    string base64Mp3 = Convert.ToBase64String(result);
            //    return Ok(base64Mp3);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
    }
}