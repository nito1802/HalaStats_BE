namespace HalaStats_BE.Dtos.Requests
{
    public class TeamResultDto
    {
        public List<PlayerDto> Players { get; set; }
        public int Goals { get; set; }
    }

    //TODO: jako wynik zwrócic tez profilowe zawodnika
}