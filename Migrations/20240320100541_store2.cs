using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanAndGoApi.Migrations
{
    /// <inheritdoc />
    public partial class store2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITEM_STORE_StoreId",
                table: "ITEM");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "ITEM",
                newName: "ShelfId");

            migrationBuilder.RenameIndex(
                name: "IX_ITEM_StoreId",
                table: "ITEM",
                newName: "IX_ITEM_ShelfId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "STORE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SHELF",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Y1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Y2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHELF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SHELF_STORE_StoreId",
                        column: x => x.StoreId,
                        principalTable: "STORE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SHELF_StoreId",
                table: "SHELF",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ITEM_SHELF_ShelfId",
                table: "ITEM",
                column: "ShelfId",
                principalTable: "SHELF",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITEM_SHELF_ShelfId",
                table: "ITEM");

            migrationBuilder.DropTable(
                name: "SHELF");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "STORE");

            migrationBuilder.RenameColumn(
                name: "ShelfId",
                table: "ITEM",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_ITEM_ShelfId",
                table: "ITEM",
                newName: "IX_ITEM_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ITEM_STORE_StoreId",
                table: "ITEM",
                column: "StoreId",
                principalTable: "STORE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
