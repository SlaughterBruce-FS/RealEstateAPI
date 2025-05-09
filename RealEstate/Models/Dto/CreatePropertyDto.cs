﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models.Dto
{
    public class CreatePropertyDto
    {
     
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Agent_Id { get; set; }

        public bool? Is_Published { get; set; }
        public bool? Is_Rent { get; set; }
        public int? Views { get; set; }
     
        public string? Prop_Type { get; set; }
        public string? Prop_Status { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Garages { get; set; }
        public int? Area { get; set; }
        public int? Lot_Size { get; set; }
        public string? Year_Built { get; set; }

        public IFormFile File { get; set; }
    }
}
