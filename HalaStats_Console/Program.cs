namespace HalaStats_Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int ratingA = 1500; // Początkowy ranking gracza A
            int ratingB = 1000; // Początkowy ranking gracza B

            // Wynik meczu: Gracz A wygrał (1), Gracz B przegrał (0)
            EloRatingSystem.UpdateRatings(1500, 1500, 1, 0);
            EloRatingSystem.UpdateRatings(1500, 1500, 0, 1);

            EloRatingSystem.UpdateRatings(1500, 1000, 1, 0);
            EloRatingSystem.UpdateRatings(1500, 1000, 0, 1);

            EloRatingSystem.UpdateRatings(2000, 1000, 1, 0);
            EloRatingSystem.UpdateRatings(2000, 1000, 0, 1);

            EloRatingSystem.UpdateRatings(3000, 1000, 1, 0);
            EloRatingSystem.UpdateRatings(3000, 1000, 0, 1);

            EloRatingSystem.UpdateRatings(1400, 1000, 1, 0);
            EloRatingSystem.UpdateRatings(1400, 1000, 0, 1);

            EloRatingSystem.UpdateRatings(1100, 1000, 1, 0);
            EloRatingSystem.UpdateRatings(1100, 1000, 0, 1);

            EloRatingSystem.UpdateRatings(2000, 1900, 1, 0);
            EloRatingSystem.UpdateRatings(2000, 1900, 0, 1);

            //Console.WriteLine($"Nowy ranking Gracza A: {ratingA}");
            //Console.WriteLine($"Nowy ranking Gracza B: {ratingB}");

            Console.WriteLine("Hello, World!");
        }
    }
}