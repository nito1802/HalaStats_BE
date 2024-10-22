using HalaStats_BE.Database;
using HalaStats_BE.Database.Entities;
using HalaStats_BE.Dtos.Responses;
using Microsoft.EntityFrameworkCore;

namespace HalaStats_BE.Services
{
    public interface IPlayerService
    {
        Task SeedPlayers();

        Task<List<PlayerRankResponseDto>> GetPlayersRank();
    }

    public class PlayerService : IPlayerService
    {
        private readonly IHalaStatsDbContext _halaStatsDbContext;

        public PlayerService(IHalaStatsDbContext halaStatsDbContext)
        {
            _halaStatsDbContext = halaStatsDbContext;
        }

        public async Task<List<PlayerRankResponseDto>> GetPlayersRank()
        {
            var playersEntitiesToRank = (await _halaStatsDbContext.Players
                .Include(a => a.Ratings)
                .ToListAsync())
                .Where(a => a.MatchIds.Any())
                .ToList();

            var playersRank = playersEntitiesToRank.Select(a => new PlayerRankResponseDto
            {
                PlayerName = a.DisplayName,
                EloRating = a.Ratings.OrderByDescending(r => r.CreatedAt).First().Rating,
                GamesCount = a.MatchIds.Count
            }).OrderByDescending(b => b.EloRating).ThenByDescending(c => c.PlayerName).ToList();


            var counterIndex = 1;
            for (int i = 0; i < playersRank.Count; i++)
            {
                var item = playersRank[i];
                var previousItem = i > 0 ? playersRank[i - 1] : null;

                if (previousItem != null && item.EloRating == previousItem.EloRating)
                {
                    item.Index = null;
                }
                else
                {
                    item.Index = counterIndex++;
                }
            }

            return playersRank;
        }

        public async Task SeedPlayers()
        {
            List<PlayerEntity> playerEntities =
            [
                new()
                {
                    Id = "Jarek Szczupak"
                },
                new()
                {
                    Id = "Krzysztof Szczupak"
                },
                new()
                {
                    Id = "Damian Lis"
                },
                new()
                {
                    Id = "Marcin Raźny"
                },
                new()
                {
                    Id = "Damian Rosół"
                },
                new()
                {
                    Id = "Kamil Rosół"
                },
                new()
                {
                    Id = "Przemo Mikrut"
                },
                new()
                {
                    Id = "Bartłomiej Bernasik"
                },
                new()
                {
                    Id = "Łukasz Hoang"
                },
                new()
                {
                    Id = "Łukasz Perzyński"
                },
                new()
                {
                    Id = "Dominik Jaskółka"
                },
                new()
                {
                    Id = "Gerwazy"
                },
                new()
                {
                    Id = "Piotr Sawicki"
                },
                new()
                {
                    Id = "Dawid Skalny"
                },
                new()
                {
                    Id = "Tomasz Marczyk"
                },
                new()
                {
                    Id = "Pan Janusz"
                },
                new()
                {
                    Id = "Martin Zap"
                },
                new()
                {
                    Id = "Konrad Chorągwicki"
                },
                new()
                {
                    Id = "Marcin Marek"
                },
            ];

            foreach (var item in playerEntities)
            {
                item.DisplayName = item.Id;
                item.Ratings = [];
                item.Ratings.Add(new EloRatingEntity
                {
                    Rating = 1500
                });
            }

            _halaStatsDbContext.Players.AddRange(playerEntities);

            await _halaStatsDbContext.SaveChangesAsync();
        }
    }
}