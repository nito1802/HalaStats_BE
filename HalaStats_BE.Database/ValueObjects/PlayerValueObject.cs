namespace HalaStats_BE.Database.ValueObjects
{
    public class PlayerValueObject
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int Difference { get; set; }

        // Możesz przechowywać obcy klucz do PlayerEntity
        public string PlayerId { get; set; } // Obcy klucz do PlayerEntity
    }
}