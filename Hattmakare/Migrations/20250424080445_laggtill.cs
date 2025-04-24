using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class laggtill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HatsMaterial",
                columns: new[] { "HatId", "MaterialId", "Quantity" },
                values: new object[] { 2, 2, 0 });

            migrationBuilder.InsertData(
                table: "OrderHats",
                columns: new[] { "Id", "HatId", "OrderId", "UserId" },
                values: new object[] { 3, 2, 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HatsMaterial",
                keyColumns: new[] { "HatId", "MaterialId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "OrderHats",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
