using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DP_Shop.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9631), null, "Sản phẩm truyền thống", "Sản phẩm truyền thống", null },
                    { 2, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9634), null, "Sản phẩm hiện đại", "Sản phẩm hiện đại", null }
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
                    { 18, "Bánh Ép Huế BBQ Vị Thịt Nướng 65G", "https://res.cloudinary.com/dlo5qxnxw/image/upload/v1734580226/chinh-300x300_yfjqj7.png" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "DeletedAt", "Description", "ExpiryDate", "Name", "Price", "Quantity", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9938), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9939), "Bánh Ép Huế Vị Thịt Nướng 40g", 30000m, 50, "banh-ep-hue-vi-thi-nuong-40g", null },
                    { 2, 2, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9952), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9953), "Bánh Ép Huế Gói Snack Vị Tép 40g", 35000m, 50, "banh-ep-hue-goi-snack-vi-tep-40g", null },
                    { 3, 2, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9956), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9956), "Bánh Ép Huế Gói Snack BBQ Vị Thịt Nướng 40g", 35000m, 50, "banh-ep-hue-goi-snack-bbq-vi-thit-nuong-40g", null },
                    { 4, 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9959), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9960), "Bánh Ép Huế Hải Sản Vị Tôm Thịt 65G", 50000m, 50, "banh-ep-hue-hai-san-vi-tom-thit-65g", null },
                    { 5, 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9963), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9963), "Bánh Ép Huế Hải Sản Vị Tôm Thịt 40G", 30000m, 50, "banh-ep-hue-hai-san-vi-tom-thit-40g", null },
                    { 6, 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9966), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9967), "Bánh ép Huế chay vị Sen Huế 65G", 50000m, 50, "banh-ep-hue-chay-vi-sen-hue-65g", null },
                    { 7, 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9971), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9971), "Bánh ép Huế chay vị Nấm Hương 65G", 50000m, 20, "banh-ep-hue-chay-vi-nam-huong-65g", null },
                    { 8, 1, new DateTime(2024, 12, 19, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9974), null, "Bánh ép Huế là một đặc sản khởi nguồn từ biển Thuận An, Huế. Bánh ép Huế là sản phẩm được chế biến theo công thức đặc biệt riêng, nhất là khi kết hợp hài hòa giữa thịt mỡ cùng với hương thơm của sả, hành, ớt, tạo nên vị ngon khó cưỡng, không thể tách rời. Bên cạnh đó, bánh có thể ăn kèm với tương ớt cay cay thì quá là ngon miệng. Bánh không chất bảo quản, không màu thực phẩm và được đóng gói trong hộp giấy đảm bảo chất lượng sản phẩm an toàn, khi được chứng nhận và cung cấp chứng chỉ an toàn vệ sinh thực phẩm HACCP.", new DateTime(2025, 1, 18, 12, 19, 24, 954, DateTimeKind.Local).AddTicks(9975), "Bánh Ép Huế BBQ Vị Thịt Nướng 65G", 50000m, 20, "banh-ep-hue-bbq-vi-thit-nuong-65g", null }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
