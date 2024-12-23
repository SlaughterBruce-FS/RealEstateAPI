using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
