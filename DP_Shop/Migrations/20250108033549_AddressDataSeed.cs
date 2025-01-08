using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class AddressDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_With_Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name_With_Type = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: false),
                    Path_With_Type = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ParentCode",
                        column: x => x.ParentCode,
                        principalTable: "Provinces",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name_With_Type = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Path_With_Type = table.Column<string>(type: "nvarchar(255)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_ParentCode",
                        column: x => x.ParentCode,
                        principalTable: "Districts",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5505));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5901), new DateTime(2025, 1, 28, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5903) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5920), new DateTime(2025, 1, 28, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5923), new DateTime(2025, 1, 28, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5924) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5926), new DateTime(2025, 2, 7, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5926) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5929), new DateTime(2025, 2, 7, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5929) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5932), new DateTime(2025, 2, 7, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5932) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5935), new DateTime(2025, 2, 7, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5938), new DateTime(2025, 2, 7, 10, 35, 45, 387, DateTimeKind.Local).AddTicks(5939) });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ParentCode",
                table: "Districts",
                column: "ParentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_ParentCode",
                table: "Wards",
                column: "ParentCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(6865));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(6867));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7185), new DateTime(2025, 1, 23, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7187) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7198), new DateTime(2025, 1, 23, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7199) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7202), new DateTime(2025, 1, 23, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7202) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7205), new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7205) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7207), new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7208) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7213), new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7213) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7215), new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7216) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7218), new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7218) });
        }
    }
}
