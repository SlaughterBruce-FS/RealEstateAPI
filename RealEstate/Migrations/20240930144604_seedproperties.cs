using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class seedproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Address", "Agent_Id", "Area", "Bathrooms", "Bedrooms", "City", "Date_listed", "Description", "Featured_Image", "Garages", "Is_Published", "Is_Rent", "Lot_Size", "Price", "Prop_Status", "Prop_Type", "Slug", "State", "Title", "Views", "Year_Built", "Zip" },
                values: new object[,]
                {
                    { 1, "1234 Main St", "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf", null, 2, 3, "San Francisco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A beautiful house for sale", "https://flawlessrealestate.blob.core.windows.net/realestate/1709011290_pexels-expect-best-323780.jpg", null, null, null, 2000, 100000.0, "For Sale", "House", "house-for-sale", "CA", "House for sale", 0, "2020", "94123" },
                    { 2, "1234 Main St", "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf", null, 2, 3, "San Francisco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A beautiful house for sale", "https://flawlessrealestate.blob.core.windows.net/realestate/1709042821_pexels-jess-loiterton-5007356.jpg", null, null, null, 2000, 100000.0, "For Sale", "House", "house-for-sale", "CA", "House for sale", 0, "2020", "94123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
