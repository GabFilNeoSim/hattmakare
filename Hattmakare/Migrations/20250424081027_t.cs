using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class t : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderHats",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OrderHats",
                columns: new[] { "Id", "HatId", "OrderId", "UserId" },
                values: new object[] { 3, 2, 1, null });
        }
    }
}
