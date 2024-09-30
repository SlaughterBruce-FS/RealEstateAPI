using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class nameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "First_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Last_Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "First_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Last_Name",
                table: "AspNetUsers");
        }
    }
}
