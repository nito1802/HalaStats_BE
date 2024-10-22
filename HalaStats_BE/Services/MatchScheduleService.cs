using HalaStats_BE.Consts;
using HalaStats_BE.Database;
using HalaStats_BE.Database.Entities;

namespace HalaStats_BE.Services
{
    public interface IMatchScheduleService
    {
        Task SeedSchedule();
    }

    public class MatchScheduleService : IMatchScheduleService
    {
        private readonly IHalaStatsDbContext _halaStatsDbContext;

        public MatchScheduleService(IHalaStatsDbContext halaStatsDbContext)
        {
            _halaStatsDbContext = halaStatsDbContext;
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

            Random random = new Random();
            var shuffledList = MatchScheduleConsts.PotentialSkarbnicy.OrderBy(x => random.Next()).ToList();
            shuffledList.Insert(0, "Damian Lis");
            shuffledList.Insert(10, "Krzysztof Szczupak");

            var skarbnikIndex = 0;
            for (int i = 0; i < matchSchedules.Count; i++)
            {
                var item = matchSchedules[i];
                item.SkarbnikId = shuffledList[skarbnikIndex++];

                if (skarbnikIndex == shuffledList.Count)
                {
                    skarbnikIndex = 0;
                }
            }

            _halaStatsDbContext.MatchSchedules.AddRange(matchSchedules);
            await _halaStatsDbContext.SaveChangesAsync();
        }
    }
}