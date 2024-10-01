using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class reseedproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Agent_Id",
                value: "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Agent_Id",
                value: "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "Agent_Id",
                value: "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Agent_Id",
                value: "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Agent_Id",
                value: "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "Agent_Id",
                value: "532d1789-c1d9-4d36-bfc1-c2bb4372c7cf");
        }
    }
}
