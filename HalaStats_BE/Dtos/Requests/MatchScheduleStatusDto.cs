using HalaStats_BE.Database.Entities;

namespace HalaStats_BE.Dtos.Requests
{
    public class MatchScheduleStatusDto
    {
        public int MatchScheduleId { get; set; }
        public EventState State { get; set; }
    }
}