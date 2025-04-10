using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class dbsets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_Addresses_AddressId",
                table: "CustomerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddress_Customers_CustomerId",
                table: "CustomerAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_HatMaterial_Hat_HatId",
                table: "HatMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_HatMaterial_Material_Materialid",
                table: "HatMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Supplier_SupplierId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHat_AspNetUsers_UserId",
                table: "OrderHat");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHat_Hat_HatId",
                table: "OrderHat");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHat_Order_OrderId",
                table: "OrderHat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHat",
                table: "OrderHat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HatMaterial",
                table: "HatMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hat",
                table: "Hat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddress",
                table: "CustomerAddress");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "OrderStatus",
                newName: "OrderStatuses");

            migrationBuilder.RenameTable(
                name: "OrderHat",
                newName: "OrderHats");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "HatMaterial",
                newName: "HatsMaterial");

            migrationBuilder.RenameTable(
                name: "Hat",
                newName: "Hats");

            migrationBuilder.RenameTable(
                name: "CustomerAddress",
                newName: "CustomerAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHat_UserId",
                table: "OrderHats",
                newName: "IX_OrderHats_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHat_OrderId",
                table: "OrderHats",
                newName: "IX_OrderHats_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHat_HatId",
                table: "OrderHats",
                newName: "IX_OrderHats_HatId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderStatusId",
                table: "Orders",
                newName: "IX_Orders_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_SupplierId",
                table: "Materials",
                newName: "IX_Materials_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_HatMaterial_Materialid",
                table: "HatsMaterial",
                newName: "IX_HatsMaterial_Materialid");

            migrationBuilder.RenameIndex(
                name: "IX_HatMaterial_HatId",
                table: "HatsMaterial",
                newName: "IX_HatsMaterial_HatId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddresses",
                newName: "IX_CustomerAddresses_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddress_AddressId",
                table: "CustomerAddresses",
                newName: "IX_CustomerAddresses_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHats",
                table: "OrderHats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HatsMaterial",
                table: "HatsMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hats",
                table: "Hats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Addresses_AddressId",
                table: "CustomerAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HatsMaterial_Hats_HatId",
                table: "HatsMaterial",
                column: "HatId",
                principalTable: "Hats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HatsMaterial_Materials_Materialid",
                table: "HatsMaterial",
                column: "Materialid",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Suppliers_SupplierId",
                table: "Materials",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHats_AspNetUsers_UserId",
                table: "OrderHats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHats_Hats_HatId",
                table: "OrderHats",
                column: "HatId",
                principalTable: "Hats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHats_Orders_OrderId",
                table: "OrderHats",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Addresses_AddressId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAddresses_Customers_CustomerId",
                table: "CustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_HatsMaterial_Hats_HatId",
                table: "HatsMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_HatsMaterial_Materials_Materialid",
                table: "HatsMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Suppliers_SupplierId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHats_AspNetUsers_UserId",
                table: "OrderHats");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHats_Hats_HatId",
                table: "OrderHats");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHats_Orders_OrderId",
                table: "OrderHats");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderStatuses",
                table: "OrderStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHats",
                table: "OrderHats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HatsMaterial",
                table: "HatsMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hats",
                table: "Hats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAddresses",
                table: "CustomerAddresses");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "OrderStatuses",
                newName: "OrderStatus");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderHats",
                newName: "OrderHat");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "HatsMaterial",
                newName: "HatMaterial");

            migrationBuilder.RenameTable(
                name: "Hats",
                newName: "Hat");

            migrationBuilder.RenameTable(
                name: "CustomerAddresses",
                newName: "CustomerAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Order",
                newName: "IX_Order_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHats_UserId",
                table: "OrderHat",
                newName: "IX_OrderHat_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHats_OrderId",
                table: "OrderHat",
                newName: "IX_OrderHat_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHats_HatId",
                table: "OrderHat",
                newName: "IX_OrderHat_HatId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_SupplierId",
                table: "Material",
                newName: "IX_Material_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_HatsMaterial_Materialid",
                table: "HatMaterial",
                newName: "IX_HatMaterial_Materialid");

            migrationBuilder.RenameIndex(
                name: "IX_HatsMaterial_HatId",
                table: "HatMaterial",
                newName: "IX_HatMaterial_HatId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddress",
                newName: "IX_CustomerAddress_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAddresses_AddressId",
                table: "CustomerAddress",
                newName: "IX_CustomerAddress_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderStatus",
                table: "OrderStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHat",
                table: "OrderHat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HatMaterial",
                table: "HatMaterial",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hat",
                table: "Hat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAddress",
                table: "CustomerAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_Addresses_AddressId",
                table: "CustomerAddress",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAddress_Customers_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HatMaterial_Hat_HatId",
                table: "HatMaterial",
                column: "HatId",
                principalTable: "Hat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HatMaterial_Material_Materialid",
                table: "HatMaterial",
                column: "Materialid",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Supplier_SupplierId",
                table: "Material",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusId",
                table: "Order",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHat_AspNetUsers_UserId",
                table: "OrderHat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHat_Hat_HatId",
                table: "OrderHat",
                column: "HatId",
                principalTable: "Hat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHat_Order_OrderId",
                table: "OrderHat",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
