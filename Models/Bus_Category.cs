using System.ComponentModel.DataAnnotations;

namespace SRC_Travel_Agencies.Models
{
    public class Bus_Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Category_Name { get; set; }
    }
}
