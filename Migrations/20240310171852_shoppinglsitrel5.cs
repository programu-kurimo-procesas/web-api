using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanAndGoApi.Migrations
{
    /// <inheritdoc />
    public partial class shoppinglsitrel5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "STORE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ITEM",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<long>(type: "bigint", nullable: false),
                    StoreId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITEM_PRODUCT_Productid",
                        column: x => x.Productid,
                        principalTable: "PRODUCT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ITEM_STORE_StoreId",
                        column: x => x.StoreId,
                        principalTable: "STORE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHOPPING_LIST",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHOPPING_LIST", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHOPPING_LIST_USER_Id",
                        column: x => x.Id,
                        principalTable: "USER",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_LIST_ASC",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<long>(type: "bigint", nullable: false),
                    ShoppingListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_LIST_ASC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUCT_LIST_ASC_PRODUCT_Productid",
                        column: x => x.Productid,
                        principalTable: "PRODUCT",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_LIST_ASC_SHOPPING_LIST_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "SHOPPING_LIST",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_Productid",
                table: "ITEM",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_StoreId",
                table: "ITEM",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_LIST_ASC_Productid",
                table: "PRODUCT_LIST_ASC",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_LIST_ASC_ShoppingListId",
                table: "PRODUCT_LIST_ASC",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_Email",
                table: "USER",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITEM");

            migrationBuilder.DropTable(
                name: "PRODUCT_LIST_ASC");

            migrationBuilder.DropTable(
                name: "STORE");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "SHOPPING_LIST");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
