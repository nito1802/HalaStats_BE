using HalaStats_BE.Database.ValueObjects;

namespace HalaStats_BE.Database.Entities
{
    public enum EventState
    {
        Scheduled = 0,
        Confirmed = 1,
        Finished = 2,
        Cancelled = 3
    }

    public class MatchScheduleEntity : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string SkarbnikId { get; set; }
        public EventState State { get; set; }

        public MatchTeamValueObject? TeamA { get; set; }
        public MatchTeamValueObject? TeamB { get; set; }
        public List<MatchEntity>? Matches { get; set; }
    }
}