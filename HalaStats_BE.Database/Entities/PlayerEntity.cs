namespace HalaStats_BE.Database.Entities
{
    public class PlayerEntity : AuditableEntity
    {
        public string Id { get; set; }
        public List<EloRatingEntity> Ratings { get; set; }
    }
}