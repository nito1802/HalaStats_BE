namespace HalaStats_BE.Dtos.Responses
{
    public class MatchResultResponseDto
    {
        public TeamResultResponseDto TeamA { get; set; }
        public TeamResultResponseDto TeamB { get; set; }
        public string EventLink { get; set; }
        public DateTime MatchDate { get; set; }
        public string Skarbnik { get; set; }
    }
}