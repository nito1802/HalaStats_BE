using FluentAssertions;
using HalaStats_Core.Models.Requests;
using HalaStats_Core.Services;

namespace HalaStats_UnitTests.Core.Services
{
    public class EloRatingServiceTests
    {
        [Theory]
        [InlineData(1500, 1500, 2, 1, 1525)]
        [InlineData(1500, 1500, 1, 2, 1475)]
        [InlineData(1500, 1500, 4, 1, 1537)]
        [InlineData(1500, 1500, 1, 4, 1463)]
        [InlineData(1500, 1500, 6, 1, 1550)]
        [InlineData(1500, 1500, 1, 6, 1450)]
        [InlineData(1500, 1500, 20, 1, 1567)]
        [InlineData(1500, 1500, 1, 20, 1433)]
        [InlineData(1800, 1800, 2, 1, 1825)]
        [InlineData(1800, 1800, 1, 2, 1775)]
        [InlineData(1500, 1000, 15, 1, 1507)]
        [InlineData(1500, 1000, 1, 15, 1373)]
        [InlineData(2000, 1000, 1, 0, 2000)]
        [InlineData(2000, 1000, 0, 1, 1951)]
        [InlineData(1100, 1000, 1, 0, 1117)]
        public void AddTest(int ratingPlayer, int ratingOppositeTeam, int goalsPlayerTeam, int goalsOppositeTeam, int expectedRatingNewPlayer)
        {
            var playerMatchResultModel = new PlayerMatchResultModel
            {
                Player = new PlayerMatchResultModel.TeamData
                {
                    Goals = goalsPlayerTeam,
                    Rating = ratingPlayer
                },
                OppositeTeam = new PlayerMatchResultModel.TeamData
                {
                    Goals = goalsOppositeTeam,
                    Rating = ratingOppositeTeam
                }
            };

            var result = new EloRatingService().UpdateRatings(playerMatchResultModel);

            result.NewRating.Should().Be(expectedRatingNewPlayer);
            result.Difference.Should().Be(expectedRatingNewPlayer - ratingPlayer);
        }
    }
}