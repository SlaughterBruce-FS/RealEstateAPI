using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Tours
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public  Properties Properties { get; set; }
        [Required]
        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public ApplicationUser User { get; set; }
        [Required]
        public DateOnly Tour_Date { get; set; }
        [Required]
        public string Time { get; set; } 
        public string Name { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Phone_Number { get; set; } 
    }

}
