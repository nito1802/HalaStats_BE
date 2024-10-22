using HalaStats_BE.Database.Entities;
using System.Text.Json.Serialization;

namespace HalaStats_BE.Dtos.Requests
{
    public class MatchScheduleStatusDto
    {
        public DateTime MatchDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EventState State { get; set; }
    }
}