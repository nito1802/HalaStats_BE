using HalaStats_Core.Models.Requests;
using HalaStats_Core.Models.Responses;

namespace HalaStats_Core.Services
{
    public interface IEloRatingService
    {
        EloPlayerRatingResponseModel UpdateRatings(PlayerMatchResultModel playerMatchResult);
    }

    public class EloRatingService : IEloRatingService
    {
        private const int K = 50; // Stała K, określa czułość rankingu na wyniki

        // Metoda obliczająca oczekiwany wynik gracza A przeciwko graczowi B
        private static double CalculateExpectedScore(int ratingA, int ratingB)
        {
            return 1.0 / (1.0 + Math.Pow(10, (ratingB - ratingA) / 400.0));
        }

        // Metoda zwracająca mnożnik na podstawie różnicy bramek
        private static double GetScoreMultiplier(int goalsA, int goalsB)
        {
            int goalDifference = Math.Abs(goalsA - goalsB);

            if (goalDifference < 3)
                return 1.0;
            else if (goalDifference < 5)
                return 1.5;
            else if (goalDifference < 10)
                return 2;
            return 2.7;
        }

        // Metoda aktualizująca ranking gracza po meczu
        public EloPlayerRatingResponseModel UpdateRatings(PlayerMatchResultModel playerMatchResult)
        {
            // Obliczenie oczekiwanych wyników
            double expected = CalculateExpectedScore(playerMatchResult.Player.Rating, playerMatchResult.OppositeTeam.Rating);

            var multiplier = GetScoreMultiplier(playerMatchResult.Player.Goals, playerMatchResult.OppositeTeam.Goals);

            int score = playerMatchResult.Player.Goals > playerMatchResult.OppositeTeam.Goals ? 1 : 0; //znormalizowana wartość wyniku

            var newRating = playerMatchResult.Player.Rating + (int)(multiplier * K * (score - expected));
            var result = new EloPlayerRatingResponseModel(playerMatchResult.Player.Rating, newRating - playerMatchResult.Player.Rating);
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"PlayerA ({scoreA} - {scoreB} PlayerB");
            //sb.AppendLine($"PlayerA: {PrintRatingChange(ratingA, newRatingA)}");
            //sb.AppendLine($"PlayerB: {PrintRatingChange(ratingB, newRatingB)}");

            Console.WriteLine($"PlayerA ({playerMatchResult.Player.Goals} - {playerMatchResult.OppositeTeam.Goals}) PlayerB");
            Console.WriteLine($"PlayerA: {PrintRatingChange(playerMatchResult.Player.Rating, playerMatchResult.OppositeTeam.Rating)}");
            //Console.WriteLine($"PlayerB: {PrintRatingChange(ratingB.OldRating, ratingB.NewRating)}");
            Console.WriteLine();
            Console.WriteLine();

            return result;
        }

        private static string PrintRatingChange(int oldRating, int newRating) => $"{oldRating} --> {newRating} ({newRating - oldRating})";
    }
}