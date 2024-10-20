using HalaStats_BE.Dtos.Requests;
using HalaStats_BE.Dtos.Responses;
using HalaStats_Core.Models.Requests;
using HalaStats_Core.Services;

namespace HalaStats_BE.Services
{
    public interface IMatchService
    {
        MatchResultResponseDto CalculatePlayersRatings(MatchResultDto matchResult);
    }

    public class MatchService : IMatchService
    {
        private readonly IEloRatingService _eloRatingService;

        public MatchService(IEloRatingService eloRatingService)
        {
            _eloRatingService = eloRatingService;
        }

        public MatchResultResponseDto CalculatePlayersRatings(MatchResultDto matchResult)
        {
            var teamBRating = (int)matchResult.TeamA.Players.Average(p => p.EloRating);
            var teamARating = (int)matchResult.TeamB.Players.Average(p => p.EloRating);

            foreach (var player in matchResult.TeamA.Players)
            {
                var playerMatchResultModel = new PlayerMatchResultModel
                {
                    Player = new()
                    {
                        Goals = matchResult.TeamA.Goals,
                        Rating = player.EloRating
                    },
                    OppositeTeam = new()
                    {
                        Goals = matchResult.TeamB.Goals,
                        Rating = teamBRating
                    }
                };

                var newRating = _eloRatingService.UpdateRatings(playerMatchResultModel);
            }

            return null;
        }
    }
}