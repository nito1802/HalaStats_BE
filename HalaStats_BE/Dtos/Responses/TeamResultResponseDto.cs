namespace HalaStats_BE.Dtos.Responses
{
    public class TeamResultResponseDto
    {
        public List<PlayerResponseDto> Players { get; set; }
        public int Goals { get; set; }
    }
}