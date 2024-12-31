using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderStatusToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8031));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8034));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8318), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8319) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8332), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8333) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8336), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8337) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8340), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8340) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8343), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8344) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8347), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8348) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8350), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8351) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8354), new DateTime(2025, 1, 29, 17, 40, 23, 163, DateTimeKind.Local).AddTicks(8355) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(2777));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3093), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3094) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3105), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3105) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3108), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3108) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3111), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3111) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3114), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3114) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3116), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3119), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3119) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2024, 12, 30, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3122), new DateTime(2025, 1, 29, 17, 33, 18, 114, DateTimeKind.Local).AddTicks(3122) });
        }
    }
}
