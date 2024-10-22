namespace HalaStats_BE.Database.Entities
{
    public class EloRatingEntity : AuditableEntity
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public DateTime MatchDate { get; set; }
    }
}