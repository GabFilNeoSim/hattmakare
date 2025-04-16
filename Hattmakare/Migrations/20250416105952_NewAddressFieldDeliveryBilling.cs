using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class NewAddressFieldDeliveryBilling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Addresses",
                newName: "DeliveryAddress");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress",
                table: "Addresses",
                newName: "StreetAddress");
        }
    }
}
