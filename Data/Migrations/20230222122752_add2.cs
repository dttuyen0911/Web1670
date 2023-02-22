using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GC02Identity.Data.Migrations
{
    public partial class add2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderID);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    pubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pubDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pubAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pubTelephone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.pubID);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    bookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookPrice = table.Column<double>(type: "float", nullable: false),
                    pubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.bookID);
                    table.ForeignKey(
                        name: "FK_books_publishers_pubID",
                        column: x => x.pubID,
                        principalTable: "publishers",
                        principalColumn: "pubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderdetails",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false),
                    bookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderdetails", x => new { x.orderID, x.bookID });
                    table.ForeignKey(
                        name: "FK_orderdetails_books_bookID",
                        column: x => x.bookID,
                        principalTable: "books",
                        principalColumn: "bookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderdetails_orders_orderID",
                        column: x => x.orderID,
                        principalTable: "orders",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_pubID",
                table: "books",
                column: "pubID");

            migrationBuilder.CreateIndex(
                name: "IX_orderdetails_bookID",
                table: "orderdetails",
                column: "bookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderdetails");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "publishers");
        }
    }
}
