using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Addresses",
                newName: "WardCode");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Addresses",
                newName: "Detail");

            migrationBuilder.AlterColumn<string>(
                name: "Path_With_Type",
                table: "Wards",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name_With_Type",
                table: "Wards",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Path_With_Type",
                table: "Districts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Districts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name_With_Type",
                table: "Districts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(777));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(780));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1234), new DateTime(2025, 1, 28, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1235) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1259), new DateTime(2025, 1, 28, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1260) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1263), new DateTime(2025, 1, 28, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1263) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1267), new DateTime(2025, 2, 7, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1268) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1271), new DateTime(2025, 2, 7, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1271) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1274), new DateTime(2025, 2, 7, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1275) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1277), new DateTime(2025, 2, 7, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1278) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "ExpiryDate" },
                values: new object[] { new DateTime(2025, 1, 8, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1281), new DateTime(2025, 2, 7, 14, 10, 55, 660, DateTimeKind.Local).AddTicks(1281) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WardCode",
                table: "Addresses",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Detail",
                table: "Addresses",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "Path_With_Type",
                table: "Wards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name_With_Type",
                table: "Wards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Path_With_Type",
                table: "Districts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "Districts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name_With_Type",
                table: "Districts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

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
        }
    }
}
