namespace HalaStats_BE.Core.Models
{
    public class PlayerRatingModel
    {
        public string PlayerId { get; set; }
        public int Difference { get; set; }
        public int NewRating { get; set; }
    }
}