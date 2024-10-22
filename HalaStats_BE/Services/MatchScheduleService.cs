using HalaStats_BE.Consts;
using HalaStats_BE.Database;
using HalaStats_BE.Database.Entities;
using HalaStats_BE.Dtos.Requests;
using HalaStats_BE.Dtos.Responses;

namespace HalaStats_BE.Services
{
    public interface IMatchScheduleService
    {
        Task SeedSchedule();

        Task<MatchScheduleResponseDto> GetMatchesSchedule();

        Task SetMatchStatus(MatchScheduleStatusDto matchScheduleStatusDto);
    }

    public class MatchScheduleService : IMatchScheduleService
    {
        private readonly IHalaStatsDbContext _halaStatsDbContext;

        public MatchScheduleService(IHalaStatsDbContext halaStatsDbContext)
        {
            _halaStatsDbContext = halaStatsDbContext;
        }

        public Task<MatchScheduleResponseDto> GetMatchesSchedule()
        {
            throw new NotImplementedException();
        }

        public async Task SeedSchedule()
        {
            List<MatchScheduleEntity> matchSchedules = [];
            foreach (var item in MatchScheduleConsts.AllSaturdaysInSeason)
            {
                matchSchedules.Add(new()
                {
                    MatchDate = DateTime.Parse(item)
                });
            }

            //Random random = new Random();
            //var skarbnicyList = MatchScheduleConsts.PotentialSkarbnicy.OrderBy(x => random.Next()).ToList();
            var skarbnicyList = MatchScheduleConsts.Skarbnicy2024To2025;

            var skarbnikIndex = 0;
            for (int i = 0; i < matchSchedules.Count; i++)
            {
                var item = matchSchedules[i];
                item.SkarbnikId = skarbnicyList[skarbnikIndex++];

                if (skarbnikIndex == skarbnicyList.Count)
                {
                    skarbnikIndex = 0;
                }
            }

            _halaStatsDbContext.MatchSchedules.AddRange(matchSchedules);
            await _halaStatsDbContext.SaveChangesAsync();
        }

        public Task SetMatchStatus(MatchScheduleStatusDto matchScheduleStatusDto)
        {
            throw new NotImplementedException();
        }
    }
}