using System.ComponentModel.DataAnnotations;

namespace HalaStats_BE.Dtos.Requests
{
    public class MatchResultDto
    {
        public TeamResultDto TeamA { get; set; }
        public TeamResultDto TeamB { get; set; }

        [Required]
        public DateTime? MatchDate { get; set; }

        [Required]
        public string? EventLink { get; set; }

        [Required]
        public string? Skarbnik { get; set; }
    }
}