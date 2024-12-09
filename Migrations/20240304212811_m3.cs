using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAC",
                table: "uploads");

            migrationBuilder.AddColumn<string>(
                name: "Air_Condition",
                table: "uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Air_Condition",
                table: "uploads");

            migrationBuilder.AddColumn<bool>(
                name: "IsAC",
                table: "uploads",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
