using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class i : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bus_Image",
                table: "uploads");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bus_Image",
                table: "uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
