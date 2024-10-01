using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class reseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "Agent_Id", "Area", "Bathrooms", "Bedrooms", "City", "Date_listed", "Description", "Featured_Image", "Garages", "Is_Published", "Is_Rent", "Lot_Size", "Price", "Prop_Status", "Prop_Type", "Slug", "State", "Title", "Views", "Year_Built", "Zip" },
                values: new object[] { 3, "1234 Main St", "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3", null, 2, 3, "San Francisco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A beautiful house for sale", "https://flawlessrealestate.blob.core.windows.net/realestate/1709042821_pexels-jess-loiterton-5007356.jpg", null, null, null, 2000, 100000.0, "For Sale", "House", "house-for-sale", "CA", "House for sale", 0, "2020", "94123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
