using System.ComponentModel.DataAnnotations;

namespace SRC_Travel_Agencies.Models
{
    public class reserve1
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int BusId { get; set; } = 0;
    }
}
