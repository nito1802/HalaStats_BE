using HalaStats_BE.Consts;
using HalaStats_BE.Database;
using HalaStats_BE.Database.Entities;
using HalaStats_BE.Dtos.Requests;
using HalaStats_BE.Dtos.Responses;
using Microsoft.EntityFrameworkCore;

namespace HalaStats_BE.Services
{
    public interface IMatchScheduleService
    {
        Task SeedSchedule();

        Task<List<MatchScheduleResponseDto>> GetMatchesSchedule();

        Task<MatchScheduleResponseDto> GetNextMatch();

        Task SetMatchStatus(MatchScheduleStatusDto matchScheduleStatusDto);
    }

    public class MatchScheduleService : IMatchScheduleService
    {
        private readonly IHalaStatsDbContext _halaStatsDbContext;

        public MatchScheduleService(IHalaStatsDbContext halaStatsDbContext)
        {
            _halaStatsDbContext = halaStatsDbContext;
        }

        public async Task SetMatchStatus(MatchScheduleStatusDto matchScheduleStatusDto)
        {
            var matchSchedule = await _halaStatsDbContext.MatchSchedules.SingleAsync(a => a.MatchDate.Date == matchScheduleStatusDto.MatchDate.Date);
            matchSchedule.State = matchScheduleStatusDto.State;

            if (matchScheduleStatusDto.State == EventState.Cancelled)
            {
                var futureMatches = await _halaStatsDbContext.MatchSchedules.Where(a => a.MatchDate > matchSchedule.MatchDate).ToListAsync();
                var futureSkarbnicy = futureMatches.Select(a => a.SkarbnikId).ToList();

                futureMatches[0].SkarbnikId = matchSchedule.SkarbnikId;
                for (var i = 1; i < futureMatches.Count; i++)
                {
                    futureMatches[i].SkarbnikId = futureSkarbnicy[i - 1];
                }
            }

            await _halaStatsDbContext.SaveChangesAsync();
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

        public async Task<List<MatchScheduleResponseDto>> GetMatchesSchedule()
        {
            var result = await _halaStatsDbContext.MatchSchedules.OrderBy(a => a.MatchDate).ToListAsync();
            return result.Select(a => new MatchScheduleResponseDto
            {
                MatchDate = a.MatchDate,
                SkarbnikId = a.SkarbnikId,
                State = a.State
            }).ToList();
        }

        public async Task<MatchScheduleResponseDto> GetNextMatch()
        {
            var result = await _halaStatsDbContext.MatchSchedules
                .OrderBy(a => a.MatchDate)
                .FirstAsync(a => a.State != EventState.Cancelled && a.State != EventState.Finished && a.MatchDate > DateTime.Now);

            return new MatchScheduleResponseDto
            {
                MatchDate = result.MatchDate,
                SkarbnikId = result.SkarbnikId,
                State = result.State
            };
        }
    }
}