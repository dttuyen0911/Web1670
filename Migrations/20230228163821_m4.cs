using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web1670.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderdetails_books_orderID",
                table: "orderdetails");

            migrationBuilder.AddColumn<double>(
                name: "amount",
                table: "orderdetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "bookID1",
                table: "orderdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "orderdetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "orderdetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orderdetails_bookID1",
                table: "orderdetails",
                column: "bookID1");

            migrationBuilder.AddForeignKey(
                name: "FK_orderdetails_books_bookID1",
                table: "orderdetails",
                column: "bookID1",
                principalTable: "books",
                principalColumn: "bookID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderdetails_books_bookID1",
                table: "orderdetails");

            migrationBuilder.DropIndex(
                name: "IX_orderdetails_bookID1",
                table: "orderdetails");

            migrationBuilder.DropColumn(
                name: "amount",
                table: "orderdetails");

            migrationBuilder.DropColumn(
                name: "bookID1",
                table: "orderdetails");

            migrationBuilder.DropColumn(
                name: "price",
                table: "orderdetails");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "orderdetails");

            migrationBuilder.AddForeignKey(
                name: "FK_orderdetails_books_orderID",
                table: "orderdetails",
                column: "orderID",
                principalTable: "books",
                principalColumn: "bookID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
