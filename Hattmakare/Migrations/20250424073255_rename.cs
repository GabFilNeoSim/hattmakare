using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HatTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Specialhatt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "HatTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Speicalhatt");
        }
    }
}
