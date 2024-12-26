using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class updateCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(5824));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(5826));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6073), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6073) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6085), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6085) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6088), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6089) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6092), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6092) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6095), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6095) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6098), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6098) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6101), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6101) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 23, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6104), new DateTime(2025, 1, 22, 10, 17, 0, 141, DateTimeKind.Local).AddTicks(6105) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9631));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9634));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9938), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9939) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9952), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9953) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9956), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9956) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9959), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9960) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9963), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9963) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9966), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9967) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9971), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9971) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9974), new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9975) });
        }
    }
}
