using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web1670.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "bookDate",
                table: "books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "bookQuantity",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookDate",
                table: "books");

            migrationBuilder.DropColumn(
                name: "bookQuantity",
                table: "books");
        }
    }
}
