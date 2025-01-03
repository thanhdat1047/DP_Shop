using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(6865), null, "Sản phẩm truyền thống", "Sản phẩm truyền thống", null },
                    { 2, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(6867), null, "Sản phẩm hiện đại", "Sản phẩm hiện đại", null }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Description", "Url" },
                values: new object[,]
                {
                    { 1, "Bánh Ép Huế Vị Thịt Nướng 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734498100/chinh-300x300_tfdduo.png" },
                    { 2, "Bánh Ép Huế Vị Thịt Nướng 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734498099/Artboard-391-300x300_o3uacb.png" },
                    { 3, "Bánh Ép Huế Gói Snack Vị Tép 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579317/2-300x300_rpepfq.jpg" },
                    { 4, "Bánh Ép Huế Gói Snack Vị Tép 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579317/3_ciytdq.jpg" },
                    { 5, "Bánh Ép Huế Gói Snack Vị Tép 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579318/Thiet-ke-chua-co-ten_w83xbi.jpg" },
                    { 6, "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/5-300x300_xkcm3z.jpg" },
                    { 7, "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/4-300x300_kkdq5f.jpg" },
                    { 8, "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/1-300x300_yn7thr.jpg" },
                    { 9, "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579658/Artboard-351-300x300_ceyyrk.png" },
                    { 10, "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579658/chinh-2-300x300_cf7cqt.png" },
                    { 11, "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579954/Artboard-361-300x300_ie5h9h.png" },
                    { 12, "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579954/chinh-3-300x300_j5x0wl.png" },
                    { 13, "Bánh ép Huế chay vị Sen Huế 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580057/Sen-768x576_mth6jv.png" },
                    { 14, "Bánh ép Huế chay vị Nấm Hương 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580131/9-300x300_rzn0md.jpg" },
                    { 15, "Bánh ép Huế chay vị Nấm Hương 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580132/12-300x300_mljbgd.jpg" },
                    { 16, "Bánh ép Huế chay vị Nấm Hương 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580133/Nam-1-300x300_vfz61t.png" },
                    { 17, "Bánh Ép Huế BBQ Vị Thịt Nướng 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580226/Artboard-391-300x300_jvirsd.png" },
                    { 18, "Bánh Ép Huế BBQ Vị Thịt Nướng 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580226/chinh-300x300_yfjqj7.png" },
                    { 19, "Bánh truyền thống", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579954/Artboard-361-300x300_ie5h9h.png" },
                    { 20, "Bánh hiện đại", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734579544/4-300x300_kkdq5f.jpg" }
                });

            migrationBuilder.InsertData(
                table: "CategoryImages",
                columns: new[] { "Id", "CategoryId", "ImageId" },
                values: new object[,]
                {
                    { 1, 1, 19 },
                    { 2, 2, 20 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "Description", "ExpiryDate", "Name", "Price", "Quantity", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7185), null, "banh-ep-hue-vi-thi-nuong-40g.txt", new DateTime(2025, 1, 23, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7187), "Bánh Ép Huế Vị Thịt Nướng 40g", 30000m, 50, "banh-ep-hue-vi-thi-nuong-40g", null },
                    { 2, 2, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7198), null, "banh-ep-hue-goi-snack-vi-tep-40g.txt", new DateTime(2025, 1, 23, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7199), "Bánh Ép Huế Gói Snack Vị Tép 40g", 35000m, 50, "banh-ep-hue-goi-snack-vi-tep-40g", null },
                    { 3, 2, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7202), null, "banh-ep-hue-goi-snack-bbq-vi-thit-nuong-40g.txt", new DateTime(2025, 1, 23, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7202), "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g", 35000m, 50, "banh-ep-hue-goi-snack-bbq-vi-thit-nuong-40g", null },
                    { 4, 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7205), null, "banh-ep-hue-hai-san-vi-tom-thit-65g.txt", new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7205), "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G", 50000m, 50, "banh-ep-hue-hai-san-vi-tom-thit-65g", null },
                    { 5, 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7207), null, "banh-ep-hue-hai-san-vi-tom-thit-40g.txt", new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7208), "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G", 30000m, 50, "banh-ep-hue-hai-san-vi-tom-thit-40g", null },
                    { 6, 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7213), null, "banh-ep-hue-chay-vi-sen-hue-65g.txt", new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7213), "Bánh ép Huế chay vị Sen Huế 65G", 50000m, 50, "banh-ep-hue-chay-vi-sen-hue-65g", null },
                    { 7, 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7215), null, "banh-ep-hue-chay-vi-nam-huong-65g.txt", new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7216), "Bánh ép Huế chay vị Nấm Hương 65G", 50000m, 20, "banh-ep-hue-chay-vi-nam-huong-65g", null },
                    { 8, 1, new DateTime(2025, 1, 3, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7218), null, "banh-ep-hue-bbq-vi-thit-nuong-65g.txt", new DateTime(2025, 2, 2, 10, 39, 1, 952, DateTimeKind.Local).AddTicks(7218), "Bánh Ép Huế BBQ Vị Thịt Nướng 65G", 50000m, 20, "banh-ep-hue-bbq-vi-thit-nuong-65g", null }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 2 },
                    { 6, 6, 3 },
                    { 7, 7, 3 },
                    { 8, 8, 3 },
                    { 9, 9, 4 },
                    { 10, 10, 4 },
                    { 11, 11, 5 },
                    { 12, 12, 5 },
                    { 13, 13, 6 },
                    { 14, 14, 7 },
                    { 15, 15, 7 },
                    { 16, 16, 7 },
                    { 17, 17, 8 },
                    { 18, 18, 8 }
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
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_CategoryId",
                table: "CategoryImages",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryImages_ImageId",
                table: "CategoryImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ImageId",
                table: "ProductImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_AddressId",
                table: "UserAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");
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
                name: "Carts");

            migrationBuilder.DropTable(
                name: "CategoryImages");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
