namespace HalaStats_Console
{
    internal class Program
    {
        private static void Main()
        {
            var dateTimeMy = DateTime.Parse("2024-10-19 20:00");

            // Dzisiejsza data
            DateTime startDate = new DateTime(2024, 10, 16);

            // Data końcowa - koniec marca
            DateTime endDate = new DateTime(2025, 3, 31);

            // Lista, która przechowa wszystkie soboty
            List<DateTime> saturdays = GetSaturdaysUntilEndOfMarch(startDate, endDate);

            // Wyświetlenie wszystkich sobót
            foreach (var saturday in saturdays)
            {
                Console.WriteLine(saturday.ToString("yyyy-MM-dd"));
            }

            var wholeText = string.Join(Environment.NewLine, saturdays.Select(a => a.ToString("yyyy-MM-dd")));
        }

        // Funkcja zwracająca listę sobót od startDate do endDate
        private static List<DateTime> GetSaturdaysUntilEndOfMarch(DateTime startDate, DateTime endDate)
        {
            List<DateTime> saturdays = new List<DateTime>();

            // Znajdź pierwszą sobotę od startDate
            DateTime current = GetNextSaturday(startDate);

            // Dodawaj soboty, dopóki nie przekroczysz endDate
            while (current <= endDate)
            {
                saturdays.Add(current);
                current = current.AddDays(7);  // Przesuń do następnej soboty
            }

            return saturdays;
        }

        // Funkcja do znalezienia najbliższej soboty od danej daty
        private static DateTime GetNextSaturday(DateTime date)
        {
            int daysUntilSaturday = ((int)DayOfWeek.Saturday - (int)date.DayOfWeek + 7) % 7;
            if (daysUntilSaturday == 0)
            {
                daysUntilSaturday = 7; // Jeśli to sobota, przesuń do następnej
            }
            return date.AddDays(daysUntilSaturday);
        }
    }
}