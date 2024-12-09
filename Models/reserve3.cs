using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRC_Travel_Agencies.Models
{
    public class reserve3
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Seat_Number { get; set; }
        [Required]
        public int Ticket_Price { get; set; }
    }
}
