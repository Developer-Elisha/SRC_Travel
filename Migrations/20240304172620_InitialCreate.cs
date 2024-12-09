using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_upload_Category_category_Id",
                table: "upload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_upload",
                table: "upload");

            migrationBuilder.RenameTable(
                name: "upload",
                newName: "uploads");

            migrationBuilder.RenameIndex(
                name: "IX_upload_category_Id",
                table: "uploads",
                newName: "IX_uploads_category_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_uploads",
                table: "uploads",
                column: "Bus_id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_uploads",
                table: "uploads");

            migrationBuilder.RenameTable(
                name: "uploads",
                newName: "upload");

            migrationBuilder.RenameIndex(
                name: "IX_uploads_category_Id",
                table: "upload",
                newName: "IX_upload_category_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_upload",
                table: "upload",
                column: "Bus_id");

            migrationBuilder.AddForeignKey(
                name: "FK_upload_Category_category_Id",
                table: "upload",
                column: "category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
