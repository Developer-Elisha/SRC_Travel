using System.ComponentModel.DataAnnotations;

namespace SRC_Travel_Agencies.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        // Properties from reserve1
        public int BookingId { get; set; }
        public int BusId { get; set; }

        // Properties from reserve2
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Estimated_Hours { get; set; }

        // Properties from reserve3
        [Required]
        public string Seat_Number { get; set; }
        [Required]
        public int Ticket_Price { get; set; }

        // Properties from reserve4
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Phone { get; set; }
    }
}
