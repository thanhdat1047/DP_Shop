using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9445));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9447));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9710), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9711) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9722), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9723) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9726), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9726) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9731), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9732) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9735), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9735) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9738), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9742), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9746), new DateTime(2025, 1, 25, 16, 16, 53, 77, DateTimeKind.Local).AddTicks(9746) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
