namespace HalaStats_BE.Dtos.Requests
{
    public class TeamResultDto
    {
        public string[] PlayerIds { get; set; }
        public int Goals { get; set; }
        public string TeamName { get; set; }
    }

    //TODO: jako wynik zwrócic tez profilowe zawodnika
}