using SRC_Travel_Agencies.Migrations;
using System.ComponentModel.DataAnnotations;

namespace SRC_Travel_Agencies.Models
{
    public class reserve4
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int phone { get; set; }

    }
}
