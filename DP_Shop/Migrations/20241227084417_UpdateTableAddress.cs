using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(2767));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(2770));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3032), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3033) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3051), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3051) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3055), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3056) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3060), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3063), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3064) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3067), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3068) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3072), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3072) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 27, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3075), new DateTime(2025, 1, 26, 15, 44, 14, 690, DateTimeKind.Local).AddTicks(3076) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3389));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3392));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3623), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3625) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3636), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3637) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3639), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3640) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3643), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3643) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3646), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3646) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3650), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3651) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3653), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3654) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 26, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3656), new DateTime(2025, 1, 25, 19, 13, 11, 300, DateTimeKind.Local).AddTicks(3657) });
        }
    }
}
