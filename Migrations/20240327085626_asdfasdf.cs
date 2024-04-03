using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScanAndGoApi.Migrations
{
    /// <inheritdoc />
    public partial class asdfasdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapUrl",
                table: "STORE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapUrl",
                table: "STORE");
        }
    }
}
