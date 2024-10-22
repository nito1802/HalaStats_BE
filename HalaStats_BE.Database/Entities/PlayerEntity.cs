namespace HalaStats_BE.Database.Entities
{
    public class PlayerEntity : AuditableEntity
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public List<EloRatingEntity> Ratings { get; set; } = [];
        public List<int> MatchIds { get; set; } = [];

        public int GetCurrentRating() => Ratings.OrderByDescending(r => r.CreatedAt).First().Rating;
    }
}