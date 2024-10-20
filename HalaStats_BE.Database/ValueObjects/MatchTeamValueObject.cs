namespace HalaStats_BE.Database.ValueObjects
{
    public class MatchTeamValueObject
    {
        public string TeamName { get; set; }
        public int Goals { get; set; }
        public int TeamRating { get; set; }
        public int Handicup { get; set; }
        public string HandicupReason { get; set; }
        public List<PlayerValueObject> Players { get; set; }
    }
}