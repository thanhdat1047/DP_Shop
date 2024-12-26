using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCategoryImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryImages_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Description", "Url" },
                values: new object[,]
                {
                    { 19, "Bánh truyền thống", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579954/Artboard-361-300x300_ie5h9h.png" },
                    { 20, "Bánh hiện đại", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/4-300x300_kkdq5f.jpg" }
                });

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

            migrationBuilder.InsertData(
                table: "CategoryImages",
                columns: new[] { "Id", "CategoryId", "ImageId" },
                values: new object[,]
                {
                    { 1, 1, 19 },
                    { 2, 2, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_CategoryId",
                table: "CategoryImages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_ImageId",
                table: "CategoryImages",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryImages");

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 20);

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
    }
}
