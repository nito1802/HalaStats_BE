﻿using HalaStats_BE.Database.ValueObjects;

namespace HalaStats_BE.Database.Entities
{
    public class MatchEntity : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string EventLink { get; set; }
        public MatchTeamValueObject TeamA { get; set; }
        public MatchTeamValueObject TeamB { get; set; }
        public string SkarbnikId { get; set; }

        public int MatchScheduleId { get; set; }
        public MatchScheduleEntity MatchSchedule { get; set; }
    }
}