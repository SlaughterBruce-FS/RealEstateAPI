using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class Properties
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public  string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public  double Price { get; set; }
        [Required]
        public string Agent_Id { get; set; }
        [ForeignKey("Agent_Id")]
        public UserProfiles User { get; set; }
        public bool? Is_Published { get; set; }
        public bool? Is_Rent { get; set; }
        public int Views { get; set; } = 0;
        public string? Slug { get; set; }
        public string? Prop_Type { get; set; }
        public string? Prop_Status { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Garages { get; set; }
        public int? Area { get; set; }
        public int? Lot_Size { get; set; }
        public string? Year_Built { get; set; }
        public DateTime Date_listed { get; set; }
        public string? Featured_Image { get; set; }

    }
}
