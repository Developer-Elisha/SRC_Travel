using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRC_Travel_Agencies.Models
{
    public class Upload_Bus
    {
        [Key]
        public int Bus_id { get; set; }
        [Required]
        public string Bus_name { get; set; }
        [Required]
        public int category_Id { get; set; }
        [Required]
        [ForeignKey("category_Id")]
        public virtual Bus_Category Category { get; set; }
        [Required]
        public int Bus_price { get; set; }
        [Required]
        public string Air_Condition { get; set; }
    }
}
