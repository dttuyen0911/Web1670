using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web1670.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookImageName",
                table: "books");

            migrationBuilder.AddColumn<DateTime>(
                name: "bookDate",
                table: "books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookDate",
                table: "books");

            migrationBuilder.AddColumn<string>(
                name: "bookImageName",
                table: "books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
