using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class booked2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserves1",
                table: "Reserves1");

            migrationBuilder.RenameTable(
                name: "Reserves1",
                newName: "Reserves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserves",
                table: "Reserves",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reserves",
                table: "Reserves");

            migrationBuilder.RenameTable(
                name: "Reserves",
                newName: "Reserves1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reserves1",
                table: "Reserves1",
                column: "BookingId");
        }
    }
}
