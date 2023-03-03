using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web1670.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_categories_cateID",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_publishers_pubID",
                table: "books");

            migrationBuilder.AlterColumn<int>(
                name: "pubID",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cateID",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_books_categories_cateID",
                table: "books",
                column: "cateID",
                principalTable: "categories",
                principalColumn: "cateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_books_publishers_pubID",
                table: "books",
                column: "pubID",
                principalTable: "publishers",
                principalColumn: "pubID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_categories_cateID",
                table: "books");

            migrationBuilder.DropForeignKey(
                name: "FK_books_publishers_pubID",
                table: "books");

            migrationBuilder.AlterColumn<int>(
                name: "pubID",
                table: "books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "cateID",
                table: "books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_books_categories_cateID",
                table: "books",
                column: "cateID",
                principalTable: "categories",
                principalColumn: "cateID");

            migrationBuilder.AddForeignKey(
                name: "FK_books_publishers_pubID",
                table: "books",
                column: "pubID",
                principalTable: "publishers",
                principalColumn: "pubID");
        }
    }
}
