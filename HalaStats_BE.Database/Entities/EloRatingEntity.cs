namespace HalaStats_BE.Database.Entities
{
    public class EloRatingEntity : AuditableEntity
    {
        public int Id { get; set; }
        public int Rating { get; set; }
    }
}