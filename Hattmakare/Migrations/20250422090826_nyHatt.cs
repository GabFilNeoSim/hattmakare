using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class nyHatt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hats",
                columns: new[] { "Id", "Comment", "Depth", "ImageUrl", "IsDeleted", "IsSpecial", "Length", "Name", "Price", "Quantity", "Size", "Width" },
                values: new object[,]
                {
                    { 1, "Testcomment", 0.0, null, false, false, 0.0, "Studenthatt", 5m, 2, 10, 0.0 },
                    { 2, "Testcomment", 0.0, null, false, false, 0.0, "Kaptenshatt", 52m, 5, 8, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderHats",
                columns: new[] { "Id", "HatId", "OrderId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, null },
                    { 2, 2, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderHats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderHats",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hats",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hats",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
