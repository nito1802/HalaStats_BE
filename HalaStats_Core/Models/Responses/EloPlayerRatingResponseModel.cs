namespace HalaStats_Core.Models.Responses
{
    public class EloPlayerRatingResponseModel
    {
        public EloPlayerRatingResponseModel(int newRating, int difference)
        {
            Difference = difference;
            NewRating = newRating + difference;
        }

        public int Difference { get; set; }
        public int NewRating { get; set; }
    }
}