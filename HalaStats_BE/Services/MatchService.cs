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
        Task<List<MatchResultResponseDto>> GetMatchesHistory();

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

        public async Task<List<MatchResultResponseDto>> GetMatchesHistory()
        {
            var result = await _halaStatsDbContext.Matches.OrderByDescending(a => a.MatchDate).Select(m => new MatchResultResponseDto
            {
                TeamA = new TeamResultResponseDto
                {
                    Goals = m.TeamA.Goals,
                    TeamRating = m.TeamA.TeamRating,
                    TeamName = m.TeamA.TeamName,
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
                    TeamRating = m.TeamB.TeamRating,
                    TeamName = m.TeamA.TeamName,
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
            List<PlayerEntity> playerEntitiesTeamA = await MapIdsToPlayerEntities(matchResult.TeamA.PlayerIds);
            List<PlayerEntity> playerEntitiesTeamB = await MapIdsToPlayerEntities(matchResult.TeamB.PlayerIds);

            var teamARating = (int)playerEntitiesTeamA.Average(p => p.GetCurrentRating());
            var teamBRating = (int)playerEntitiesTeamB.Average(p => p.GetCurrentRating());

            //TODO: dla nieparzystej liczby graczy ustawiac handicup -100 pkt dla zespolu z mniejsza liczba graczy

            List<PlayerRatingModel> playerRatingsTeamA = [];
            foreach (var player in playerEntitiesTeamA)
            {
                var playerMatchResultModel = new PlayerMatchResultModel
                {
                    Player = new()
                    {
                        Goals = matchResult.TeamA.Goals,
                        Rating = player.GetCurrentRating()
                    },
                    OppositeTeam = new()
                    {
                        Goals = matchResult.TeamB.Goals,
                        Rating = teamBRating
                    },
                    MatchDate = matchResult.MatchDate
                };
                var newRating = UpdatePlayerEntity(player, playerMatchResultModel);
                playerRatingsTeamA.Add(new PlayerRatingModel
                {
                    PlayerId = player.Id,
                    NewRating = newRating.NewRating,
                    Difference = newRating.Difference
                });
            }

            List<PlayerRatingModel> playerRatingsTeamB = [];
            foreach (var player in playerEntitiesTeamB)
            {
                var playerMatchResultModel = new PlayerMatchResultModel
                {
                    Player = new()
                    {
                        Goals = matchResult.TeamB.Goals,
                        Rating = player.GetCurrentRating()
                    },
                    OppositeTeam = new()
                    {
                        Goals = matchResult.TeamA.Goals,
                        Rating = teamARating
                    },
                    MatchDate = matchResult.MatchDate
                };
                var newRating = UpdatePlayerEntity(player, playerMatchResultModel);
                playerRatingsTeamB.Add(new PlayerRatingModel
                {
                    PlayerId = player.Id,
                    NewRating = newRating.NewRating,
                    Difference = newRating.Difference
                });
            }

            var matchSchedule = await _halaStatsDbContext.MatchSchedules.SingleAsync(a => a.MatchDate.Date == matchResult.MatchDate!.Value.Date);

            var matchEntity = new MatchEntity
            {
                TeamA = new()
                {
                    TeamName = matchResult.TeamA.TeamName,
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
                    TeamName = matchResult.TeamB.TeamName,
                    Goals = matchResult.TeamB.Goals,
                    TeamRating = teamBRating,
                    Players = playerRatingsTeamB.Select(a => new PlayerValueObject
                    {
                        PlayerId = a.PlayerId,
                        Difference = a.Difference,
                        Rating = a.NewRating
                    }).ToList()
                },
                EventLink = matchResult.EventLink!,
                MatchDate = matchResult.MatchDate!.Value,
                SkarbnikId = matchResult.Skarbnik!,
                MatchScheduleId = matchSchedule.Id
            };

            _halaStatsDbContext.Matches.Add(matchEntity);

            await _halaStatsDbContext.SaveChangesAsync();

            foreach (var item in playerEntitiesTeamA)
            {
                item.MatchIds.Add(matchEntity.Id);
            }
            foreach (var item in playerEntitiesTeamB)
            {
                item.MatchIds.Add(matchEntity.Id);
            }
            await _halaStatsDbContext.SaveChangesAsync();
        }

        private async Task<List<PlayerEntity>> MapIdsToPlayerEntities(string[] playerIds)
        {
            var result = new List<PlayerEntity>();

            foreach (var playerId in playerIds)
            {
                var playerEntity = await _halaStatsDbContext.Players.Include(a => a.Ratings).FirstOrDefaultAsync(a => a.Id == playerId);
                if (playerEntity != null)
                {
                    result.Add(playerEntity);
                }
            }

            return result;
        }

        private EloPlayerRatingResponseModel UpdatePlayerEntity(PlayerEntity player, PlayerMatchResultModel playerMatchResultModel)
        {
            var newRating = _eloRatingService.UpdateRatings(playerMatchResultModel);

            var newRatingEntity = new EloRatingEntity
            {
                Rating = newRating.NewRating,
                MatchDate = playerMatchResultModel.MatchDate!.Value
            };

            player.Ratings.Add(newRatingEntity);

            return newRating;
        }
    }
}