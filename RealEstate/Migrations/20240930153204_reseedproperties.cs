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
                value: "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Agent_Id",
                value: "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "Agent_Id",
                value: "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Agent_Id",
                value: "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Agent_Id",
                value: "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3,
                column: "Agent_Id",
                value: "6c6b8e57-ffd9-4d3a-9a97-29ba5cb2d2d3");
        }
    }
}
