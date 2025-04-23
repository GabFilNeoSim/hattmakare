using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class NewDbFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHats_Orders_OrderId",
                table: "OrderHats");

            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "Hats");

            migrationBuilder.AddColumn<int>(
                name: "HatTypeId",
                table: "Hats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HatTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HatTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Hats",
                keyColumn: "Id",
                keyValue: 1,
                column: "HatTypeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Hats",
                keyColumn: "Id",
                keyValue: 2,
                column: "HatTypeId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Hats_HatTypeId",
                table: "Hats",
                column: "HatTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hats_HatTypes_HatTypeId",
                table: "Hats",
                column: "HatTypeId",
                principalTable: "HatTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHats_Orders_OrderId",
                table: "OrderHats",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hats_HatTypes_HatTypeId",
                table: "Hats");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHats_Orders_OrderId",
                table: "OrderHats");

            migrationBuilder.DropTable(
                name: "HatTypes");

            migrationBuilder.DropIndex(
                name: "IX_Hats_HatTypeId",
                table: "Hats");

            migrationBuilder.DropColumn(
                name: "HatTypeId",
                table: "Hats");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "Hats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Hats",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsSpecial",
                value: false);

            migrationBuilder.UpdateData(
                table: "Hats",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsSpecial",
                value: false);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHats_Orders_OrderId",
                table: "OrderHats",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
