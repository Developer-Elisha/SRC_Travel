using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Upload_BusBus_id",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_Upload_BusBus_id",
                table: "Category",
                column: "Upload_BusBus_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_uploads_Upload_BusBus_id",
                table: "Category",
                column: "Upload_BusBus_id",
                principalTable: "uploads",
                principalColumn: "Bus_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_uploads_Upload_BusBus_id",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_Upload_BusBus_id",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Upload_BusBus_id",
                table: "Category");
        }
    }
}
