﻿namespace HalaStats_Core.Models.Requests
{
    public class PlayerMatchResultModel
    {
        public TeamData Player { get; set; }
        public TeamData OppositeTeam { get; set; }

        public class TeamData
        {
            public int Goals { get; set; }
            public int Rating { get; set; }
        }
    }
}