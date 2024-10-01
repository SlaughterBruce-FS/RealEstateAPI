using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Models;

namespace RealEstate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Properties> Properties { get; set; }
        public DbSet<Tours> Tours { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Properties>().HasData(
                new Properties
                {
                    Id = 1,
                    Title = "House for sale",
                    Description = "A beautiful house for sale",
                    Price = 100000,
                    Address = "1234 Main St",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94123",
                    Year_Built = "2020",
                    Bedrooms = 3,
                    Bathrooms = 2,
                    Prop_Type = "House",
                    Prop_Status = "For Sale",
                    Lot_Size = 2000,
                    Views = 0,
                    Slug = "house-for-sale",
                    Agent_Id = "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf",
                    Featured_Image= "https://flawlessrealestate.blob.core.windows.net/realestate/1709011290_pexels-expect-best-323780.jpg"
                },
                 new Properties
                 {
                     Id = 2,
                     Title = "House for sale",
                     Description = "A beautiful house for sale",
                     Price = 100000,
                     Address = "1234 Main St",
                     City = "San Francisco",
                     State = "CA",
                     Zip = "94123",
                     Year_Built = "2020",
                     Bedrooms = 3,
                     Bathrooms = 2,
                     Prop_Type = "House",
                     Prop_Status = "For Sale",
                     Lot_Size = 2000,
                     Views = 0,
                     Slug = "house-for-sale",
                     Agent_Id = "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf",
                     Featured_Image = "https://flawlessrealestate.blob.core.windows.net/realestate/1709042821_pexels-jess-loiterton-5007356.jpg"
                 }
                 ,
                 new Properties
                 {
                     Id = 3,
                     Title = "House for sale",
                     Description = "A beautiful house for sale",
                     Price = 100000,
                     Address = "1234 Main St",
                     City = "San Francisco",
                     State = "CA",
                     Zip = "94123",
                     Year_Built = "2020",
                     Bedrooms = 3,
                     Bathrooms = 2,
                     Prop_Type = "House",
                     Prop_Status = "For Sale",
                     Lot_Size = 2000,
                     Views = 0,
                     Slug = "house-for-sale",
                     Agent_Id = "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf",
                     Featured_Image = "https://flawlessrealestate.blob.core.windows.net/realestate/1709042821_pexels-jess-loiterton-5007356.jpg"
                 }
                );
        }


    }
}
