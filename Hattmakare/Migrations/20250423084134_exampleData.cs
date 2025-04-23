using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class exampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HatTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Standardhatt" },
                    { 2, "Standardhatt med tillägg" },
                    { 3, "Speicalhatt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HatTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HatTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HatTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
