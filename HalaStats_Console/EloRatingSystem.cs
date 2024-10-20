using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalaStats_Console
{
    public class EloRatingSystem
    {
        private const int K = 32; // Stała K, określa czułość rankingu na wyniki

        // Metoda obliczająca oczekiwany wynik gracza A przeciwko graczowi B
        private static double CalculateExpectedScore(int ratingA, int ratingB)
        {
            return 1.0 / (1.0 + Math.Pow(10, (ratingB - ratingA) / 400.0));
        }

        // Metoda zwracająca mnożnik na podstawie różnicy bramek
        private static double GetScoreMultiplier(int scoreA, int scoreB)
        {
            int goalDifference = Math.Abs(scoreA - scoreB);

            if (goalDifference < 3)
                return 1.0;  // najmniejsza zmiana rankingowa
            else if (goalDifference < 5)
                return 1.5;  // średnia zmiana rankingowa
            else if (goalDifference < 10)
                return 2;  // średnia zmiana rankingowa
            return 3.0;  // większa zmiana rankingowa dla różnicy 3 i wyższej
        }

        // Metoda aktualizująca ranking gracza po meczu
        public static void UpdateRatings(int ratingA, int ratingB, int scoreA, int scoreB)
        {
            // Obliczenie oczekiwanych wyników
            double expectedA = CalculateExpectedScore(ratingA, ratingB);
            double expectedB = CalculateExpectedScore(ratingB, ratingA);

            // Aktualizacja rankingów na podstawie rzeczywistych wyników
            var newRatingA = (int)(ratingA + K * (scoreA - expectedA));

            var multiplier = GetScoreMultiplier(scoreA, scoreB);

            var newRatingAxx = (int)(multiplier * K * (scoreA - expectedA));

            var newRatingB = (int)(ratingB + K * (scoreB - expectedB));

            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine($"PlayerA ({scoreA} - {scoreB} PlayerB");
            //sb.AppendLine($"PlayerA: {PrintRatingChange(ratingA, newRatingA)}");
            //sb.AppendLine($"PlayerB: {PrintRatingChange(ratingB, newRatingB)}");

            Console.WriteLine($"PlayerA ({scoreA} - {scoreB} PlayerB");
            Console.WriteLine($"PlayerA: {PrintRatingChange(ratingA, newRatingA)}");
            Console.WriteLine($"PlayerB: {PrintRatingChange(ratingB, newRatingB)}");
            Console.WriteLine();
            Console.WriteLine();
        }

        private static string PrintRatingChange(int oldRating, int newRating) => $"{oldRating} --> {newRating} ({newRating - oldRating})";
    }
}