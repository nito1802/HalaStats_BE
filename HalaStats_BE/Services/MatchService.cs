using HalaStats_BE.Dtos.Requests;
using HalaStats_Core.Services;

namespace HalaStats_BE.Services
{
    public interface IMatchService
    {
        MatchResultDto CalculatePlayersRatings(MatchResultDto matchResult);
    }

    public class MatchService : IMatchService
    {
        private readonly IEloRatingService _eloRatingService;

        public MatchService(IEloRatingService eloRatingService)
        {
            _eloRatingService = eloRatingService;
        }

        public MatchResultDto CalculatePlayersRatings(MatchResultDto matchResult)
        {
            var teamBRating = matchResult.TeamA.Players.Average(p => p.EloRating);
            var teamARating = matchResult.TeamB.Players.Average(p => p.EloRating);

            return null;
        }
    }
}