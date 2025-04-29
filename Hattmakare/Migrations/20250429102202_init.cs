using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hattmakare.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillingAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDecoration = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HeadMeasurements = table.Column<double>(type: "float", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Depth = table.Column<double>(type: "float", nullable: false),
                    HatTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hats_HatTypes_HatTypeId",
                        column: x => x.HatTypeId,
                        principalTable: "HatTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    OrderStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HatsMaterial",
                columns: table => new
                {
                    HatId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HatsMaterial", x => new { x.HatId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_HatsMaterial_Hats_HatId",
                        column: x => x.HatId,
                        principalTable: "Hats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HatsMaterial_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderHats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HatId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHats_Hats_HatId",
                        column: x => x.HatId,
                        principalTable: "Hats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHats_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "BillingAddress", "City", "Country", "DeliveryAddress", "PostalCode" },
                values: new object[,]
                {
                    { 1, "Blåbärsstigen 99", "Örebro", "Sverige", "Solrosvägen 88", "11322" },
                    { 2, "Krickelinsväg 101", "Stockholm", "Sverige", "Snöflingegatan 202", "55667" },
                    { 3, "Månstrålevägen 45", "Umeå", "Sverige", "Regnbågsgatan 12", "22433" },
                    { 4, "Silverbäcksvägen 77", "Paris", "Frankrike", "Älvdalsvägen 34", "77889" }
                });

            migrationBuilder.InsertData(
                table: "HatTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Standardhatt" },
                    { 2, "Standardhatt med tillägg" },
                    { 3, "Specialhatt" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "IsDecoration", "Name", "Price", "Supplier", "Unit" },
                values: new object[,]
                {
                    { 1, false, "Ullfilt", 124.50m, "Nordic Felt AB", "m" },
                    { 2, false, "Kaninfilt", 499.90m, "Hatter’s Choice Ltd", "m" },
                    { 3, false, "Toquillastrå", 419.75m, "Ecuador Weaves Co.", "kg" },
                    { 4, false, "Rishalm", 109.20m, "StrawCraft Asia", "kg" },
                    { 5, false, "Palmlöv", 94.60m, "Tropic Naturals", "kg" },
                    { 6, false, "Majsblad", 134.95m, "EcoFiber Mexico", "kg" },
                    { 7, false, "Hampfibrer", 159.50m, "GreenTextiles GmbH", "kg" },
                    { 8, false, "Bomull", 59.90m, "CottonLine Textiles", "m" },
                    { 9, false, "Linne", 84.75m, "NordTextil AB", "m" },
                    { 10, false, "Ull", 109.00m, "NordTextil AB", "m" },
                    { 11, false, "Silke", 399.90m, "Silken Touch Co.", "m" },
                    { 12, false, "Satin", 129.50m, "Fabric Elegance", "m" },
                    { 13, false, "Tweed", 179.90m, "Highland Textiles", "m" },
                    { 14, false, "Polyester", 49.95m, "GlobalPoly Ltd", "m" },
                    { 15, false, "Läder", 849.00m, "Scandi Leatherworks", "st" },
                    { 16, false, "Lackerat papper", 0.75m, "CraftMaterials.se", "st" },
                    { 17, false, "Fuskpäls", 144.60m, "FauxFur Fabrics", "m" },
                    { 18, true, "Strutsfjäder", 35.00m, "Bendigo Farm Inc", "st" },
                    { 19, true, "Påfågelfjäder", 64.90m, "FeatherWorks Ltd", "st" },
                    { 20, true, "Hönsfjäder", 0.90m, "FeatherWorks Ltd", "st" },
                    { 21, true, "Tygblommor", 29.95m, "DecoFlora AB", "st" },
                    { 22, true, "Pärlor", 11.50m, "Pearl Paradise", "g" },
                    { 23, true, "Spets", 39.90m, "Lace & Grace", "m" },
                    { 24, true, "Lurextråd", 59.00m, "ShinyThreads Co.", "m" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ej påbörjad" },
                    { 2, "Påbörjad" },
                    { 3, "Klar" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "Email", "FirstName", "HeadMeasurements", "IsDeleted", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "olof.svensson@epost.se", "Olof", 0.0, false, "Svensson", "0723338282" },
                    { 2, 2, "jan.jansson@epost.se", "Jan", 0.0, false, "Jansson", "0723557781" },
                    { 3, 3, "julia.smith@epost.se", "Julia", 0.0, false, "Smith", "0723219981" },
                    { 4, 4, "kajsa.fisk@epost.se", "Kajsa", 0.0, false, "Fisk", "0733447785" }
                });

            migrationBuilder.InsertData(
                table: "Hats",
                columns: new[] { "Id", "Comment", "Depth", "HatTypeId", "ImageUrl", "IsDeleted", "Length", "Name", "Price", "Quantity", "Size", "Width" },
                values: new object[,]
                {
                    { 1, "En vit, rund mössa med svart skärm och en kokard framtill, traditionellt buren vid svenska studentexamina", 5.0, 1, "Student.jpg", false, 23.0, "Studenthatt", 1500m, 2, 10, 20.0 },
                    { 2, "En formell vit hatt med svart skärm och guldbroderad dekoration, som symboliserar sjöfartsbefäl.", 4.0, 1, "Kaptenshatt.jpg", false, 22.0, "Kaptenshatt", 1000m, 3, 9, 18.0 },
                    { 3, "En röd, cylinderformad hatt utan brätten, ofta prydd med en tofs, traditionellt buren i delar av Mellanöstern och Nordafrika.", 9.0, 1, "Fez.jpg", false, 16.0, "Fez", 800m, 2, 8, 16.0 },
                    { 4, "En bredbrättad hatt av filt eller halm, designad för att skydda mot sol och regn på den amerikanska prärien.", 8.0, 1, "Cowboyhatt.jpg", false, 35.0, "Cowboyhatt", 2000m, 1, 12, 30.0 },
                    { 5, "En lätt och luftig hatt flätad av halm, perfekt för att ge skugga och svalka under soliga sommardagar.", 7.0, 1, "Halmhatt.jpg", false, 32.0, "Halmhatt", 600m, 1, 11, 30.0 }
                });

            migrationBuilder.InsertData(
                table: "HatsMaterial",
                columns: new[] { "HatId", "MaterialId", "Quantity" },
                values: new object[,]
                {
                    { 1, 8, 0.5 },
                    { 1, 12, 0.29999999999999999 },
                    { 1, 21, 1.0 },
                    { 1, 22, 5.0 },
                    { 2, 8, 0.5 },
                    { 2, 12, 0.29999999999999999 },
                    { 2, 24, 1.0 },
                    { 3, 10, 0.69999999999999996 },
                    { 3, 24, 0.5 },
                    { 4, 5, 0.5 },
                    { 4, 15, 1.0 },
                    { 4, 19, 1.0 },
                    { 5, 4, 0.69999999999999996 },
                    { 5, 20, 2.0 },
                    { 5, 21, 1.0 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DiscountPercentage", "EndDate", "OrderStatusId", "Price", "Priority", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3125m, false, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, null, new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6450m, true, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, null, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 600m, true, new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 3, null, new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 600m, false, new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 4, null, new DateTime(2025, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 600m, true, new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 4, null, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 600m, false, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OrderHats",
                columns: new[] { "Id", "HatId", "OrderId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, null },
                    { 2, 2, 1, null },
                    { 3, 5, 2, null },
                    { 4, 4, 2, null },
                    { 5, 2, 2, null },
                    { 6, 2, 3, null },
                    { 7, 3, 4, null },
                    { 8, 5, 4, null },
                    { 9, 2, 5, null },
                    { 10, 4, 5, null },
                    { 11, 1, 6, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Hats_HatTypeId",
                table: "Hats",
                column: "HatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HatsMaterial_MaterialId",
                table: "HatsMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHats_HatId",
                table: "OrderHats",
                column: "HatId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHats_OrderId",
                table: "OrderHats",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHats_UserId",
                table: "OrderHats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HatsMaterial");

            migrationBuilder.DropTable(
                name: "OrderHats");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Hats");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "HatTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
