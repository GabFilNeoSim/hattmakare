using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class hatt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderHats",
                columns: new[] { "Id", "HatId", "OrderId", "UserId" },
                values: new object[] { 3, 2, 2, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderHats",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
