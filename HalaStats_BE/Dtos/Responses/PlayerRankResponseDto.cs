namespace HalaStats_BE.Dtos.Responses
{
    public class PlayerRankResponseDto
    {
        public int? Index { get; set; }
        public string PlayerName { get; set; }
        public int EloRating { get; set; }
        public int GamesCount { get; set; }
    }
}