﻿using HalaStats_BE.Database.Entities;

namespace HalaStats_BE.Dtos.Responses
{
    public class MatchScheduleResponseDto
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string SkarbnikId { get; set; }
        public EventState State { get; set; }
        //public MatchTeamValueObject? TeamA { get; set; } //dodać potencjalne składy
        //public MatchTeamValueObject? TeamB { get; set; }
    }
}