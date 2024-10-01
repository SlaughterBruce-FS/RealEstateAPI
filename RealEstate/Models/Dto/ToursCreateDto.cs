using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.Dto
{
    public class ToursCreateDto
    {
       
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public string AgentId { get; set; }
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
