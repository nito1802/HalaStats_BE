namespace HalaStats_Core.Models.Requests
{
    public class EloMatchRatingModel
    {
        public PlayerMatchResultModel TeamA { get; set; }
        public PlayerMatchResultModel TeamB { get; set; }
    }
}