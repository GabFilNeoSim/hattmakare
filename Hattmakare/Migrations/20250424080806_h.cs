using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class h : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HatsMaterial",
                keyColumns: new[] { "HatId", "MaterialId" },
                keyValues: new object[] { 1, 1 },
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "HatsMaterial",
                keyColumns: new[] { "HatId", "MaterialId" },
                keyValues: new object[] { 2, 2 },
                column: "Quantity",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unit",
                value: "M");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HatsMaterial",
                keyColumns: new[] { "HatId", "MaterialId" },
                keyValues: new object[] { 1, 1 },
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "HatsMaterial",
                keyColumns: new[] { "HatId", "MaterialId" },
                keyValues: new object[] { 2, 2 },
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unit",
                value: "3");
        }
    }
}
