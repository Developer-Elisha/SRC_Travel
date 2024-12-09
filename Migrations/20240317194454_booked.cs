using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SRC_Travel_Agencies.Migrations
{
    /// <inheritdoc />
    public partial class booked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Estimated_Hours",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Seat_Number",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "TO",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Ticket_Price",
                table: "Reserves");

            migrationBuilder.CreateTable(
                name: "Reserves2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    Estimated_Hours = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reserves3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seat_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ticket_Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reserves4",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves4", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserves2");

            migrationBuilder.DropTable(
                name: "Reserves3");

            migrationBuilder.DropTable(
                name: "Reserves4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reserves",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Distance",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estimated_Hours",
                table: "Reserves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Reserves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Seat_Number",
                table: "Reserves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TO",
                table: "Reserves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Ticket_Price",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
