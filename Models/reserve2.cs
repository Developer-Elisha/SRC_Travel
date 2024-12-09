using System.ComponentModel.DataAnnotations;

namespace SRC_Travel_Agencies.Models
{
    public class reserve2
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string TO { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Estimated_Hours { get; set; }
    }
}
