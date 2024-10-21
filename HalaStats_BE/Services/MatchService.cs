using HalaStats_BE.Core.Models;
using HalaStats_BE.Database;
using HalaStats_BE.Database.Entities;
using HalaStats_BE.Database.ValueObjects;
using HalaStats_BE.Dtos.Requests;
using HalaStats_BE.Dtos.Responses;
using HalaStats_Core.Models.Requests;
using HalaStats_Core.Models.Responses;
using HalaStats_Core.Services;
using Microsoft.EntityFrameworkCore;

namespace HalaStats_BE.Services
{
    public interface IMatchService
    {
        Task<List<MatchResultResponseDto>> GetMatchesHistory(MatchResultDto matchResult);

        Task CalculatePlayersRatings(MatchResultDto matchResult);
    }

    public class MatchService : IMatchService
    {
        private readonly IEloRatingService _eloRatingService;
        private readonly IHalaStatsDbContext _halaStatsDbContext;

        public MatchService(IEloRatingService eloRatingService, IHalaStatsDbContext halaStatsDbContext)
        {
            _eloRatingService = eloRatingService;
            _halaStatsDbContext = halaStatsDbContext;
        }

        public async Task<List<MatchResultResponseDto>> GetMatchesHistory(MatchResultDto matchResult)
        {
            var result = await _halaStatsDbContext.Matches.Select(m => new MatchResultResponseDto
            {
                TeamA = new TeamResultResponseDto
                {
                    Goals = m.TeamA.Goals,
                    Players = m.TeamA.Players.Select(p => new PlayerResponseDto
                    {
                        Id = p.PlayerId,
                        NewRating = p.Rating,
                        Difference = p.Difference
                    }).ToList()
                },
                TeamB = new TeamResultResponseDto
                {
                    Goals = m.TeamB.Goals,
                    Players = m.TeamB.Players.Select(p => new PlayerResponseDto
                    {
                        Id = p.PlayerId,
                        NewRating = p.Rating,
                        Difference = p.Difference
                    }).ToList()
                },
                EventLink = m.EventLink,
                MatchDate = m.MatchDate,
                Skarbnik = m.SkarbnikId
            }).ToListAsync();

            return result;
        }

        //TODO: Calculate nie powinno zwracać wyniki (tylko być Postem bezwynikowym) wynik zwracać GETem
        public async Task CalculatePlayersRatings(MatchResultDto matchResult)
        {
            var teamBRating = (int)matchResult.TeamA.Players.Average(p => p.EloRating);
            var teamARating = (int)matchResult.TeamB.Players.Average(p => p.EloRating);

            //TODO: dla nieparzystej liczby graczy ustawiac handicup -100 pkt dla zespolu z mniejsza liczba graczy

            List<PlayerRatingModel> playerRatingsTeamA = [];
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
                var newRating = await UpdatePlayerEntity(player, playerMatchResultModel);
                playerRatingsTeamA.Add(new PlayerRatingModel
                {
                    PlayerId = player.Id,
                    NewRating = newRating.NewRating,
                    Difference = newRating.Difference
                });
            }

            List<PlayerRatingModel> playerRatingsTeamB = [];
            foreach (var player in matchResult.TeamB.Players)
            {
                var playerMatchResultModel = new PlayerMatchResultModel
                {
                    Player = new()
                    {
                        Goals = matchResult.TeamB.Goals,
                        Rating = player.EloRating
                    },
                    OppositeTeam = new()
                    {
                        Goals = matchResult.TeamA.Goals,
                        Rating = teamBRating
                    }
                };
                var newRating = await UpdatePlayerEntity(player, playerMatchResultModel);
                playerRatingsTeamB.Add(new PlayerRatingModel
                {
                    PlayerId = player.Id,
                    NewRating = newRating.NewRating,
                    Difference = newRating.Difference
                });
            }

            _halaStatsDbContext.Matches.Add(new MatchEntity
            {
                TeamA = new()
                {
                    TeamName = "Team Złoty",
                    Goals = matchResult.TeamA.Goals,
                    TeamRating = teamARating,
                    Players = playerRatingsTeamA.Select(a => new PlayerValueObject
                    {
                        PlayerId = a.PlayerId,
                        Difference = a.Difference,
                        Rating = a.NewRating
                    }).ToList()
                },
                TeamB = new()
                {
                    TeamName = "Team Czarny",
                    Goals = matchResult.TeamB.Goals,
                    TeamRating = teamBRating,
                    Players = playerRatingsTeamB.Select(a => new PlayerValueObject
                    {
                        PlayerId = a.PlayerId,
                        Difference = a.Difference,
                        Rating = a.NewRating
                    }).ToList()
                },
                EventLink = "Link do FEJSA",
                MatchDate = new DateTime(2024, 10, 10),
                SkarbnikId = "Damian Lis"
            });

            await _halaStatsDbContext.SaveChangesAsync();
        }

        private async Task<EloPlayerRatingResponseModel> UpdatePlayerEntity(PlayerDto player, PlayerMatchResultModel playerMatchResultModel)
        {
            var newRating = _eloRatingService.UpdateRatings(playerMatchResultModel);

            //zaseedowc tabele Players

            var newRatingEntity = new EloRatingEntity
            {
                Rating = newRating.NewRating
            };

            (await _halaStatsDbContext.Players.FindAsync(player.Id)).Ratings.Add(newRatingEntity);

            return newRating;
        }
    }
}