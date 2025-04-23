using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class hatt3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "Email", "FirstName", "HeadMeasurements", "IsDeleted", "LastName", "PhoneNumber" },
                values: new object[] { 2, 1, "testmejl2", "Folo", 0.0, false, "Nossnevs", "1234567891" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "EndDate", "OrderStatusId", "Price", "Priority", "StartDate" },
                values: new object[] { 3, 2, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 600m, true, new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "OrderHats",
                columns: new[] { "Id", "HatId", "OrderId", "UserId" },
                values: new object[] { 4, 2, 3, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderHats",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
