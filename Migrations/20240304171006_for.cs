using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class @for : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bus_category",
                table: "uploads");

            migrationBuilder.AddColumn<int>(
                name: "category_Id",
                table: "uploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_uploads_category_Id",
                table: "uploads",
                column: "category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_uploads_Category_category_Id",
                table: "uploads",
                column: "category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_uploads_Category_category_Id",
                table: "uploads");

            migrationBuilder.DropIndex(
                name: "IX_uploads_category_Id",
                table: "uploads");

            migrationBuilder.DropColumn(
                name: "category_Id",
                table: "uploads");

            migrationBuilder.AddColumn<string>(
                name: "Bus_category",
                table: "uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
