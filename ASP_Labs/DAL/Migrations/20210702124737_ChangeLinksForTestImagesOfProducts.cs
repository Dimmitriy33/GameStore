using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApp.DAL.Migrations
{
    public partial class ChangeLinksForTestImagesOfProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("02811f45-9240-4f3d-a13f-361c0ca693e8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("185a0529-c29f-40b5-8e20-134aab3f80be"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1b27708f-f9da-4a83-9d54-041cd2889101"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("3e2bb91e-fb78-467f-a553-447cbd149f36"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4857911c-cf2f-4803-87f0-bd99437eeeb8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4d393685-ac1f-4fd4-b6a1-f5b6f8df39de"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("52b2bbf2-5e51-4789-9ddf-bc6d39f33b73"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("70bd5995-fe29-4b93-8425-5a12e961c68b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("789e46b7-6d01-40dc-9ad6-0469208097ba"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("832ff5f9-06c2-406e-8d4c-87e83a6a4aec"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("92671361-3c28-4e43-9175-3a6f31ef1ef1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a9a3a4db-3947-4a77-a8b6-27571ac1d3a1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ae9993ac-2baa-4445-85a4-70097a1776cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("dccff4b0-23e8-479e-8091-cd1145a5cd36"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f092c4d6-5e3c-4860-a8cb-c8e14eae5e8d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ff8d5672-43f3-47e8-afa6-673e2ba5759e"));

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRating",
                table: "Products",
                type: "decimal(4,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Background", "Count", "Genre", "Logo", "Name", "Platform", "Price", "Rating", "TotalRating" },
                values: new object[,]
                {
                    { new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1090, "Esports", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png", "FIFA 2020", 1, 10m, 0, 7.32m },
                    { new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1080, "Strategy", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229190/ASP_Labs/D0_9B_D0_BE_D0_B3_D0_BE_D1_82_D0_B8_D0_BF__D0_B8_D0_B3_D1_80_D1_8B_God_of_War_mgno9l.png", "God of War", 1, 20m, 16, 8.31m },
                    { new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1070, "Action", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229196/ASP_Labs/D0_9E_D0_B1_D0_BB_D0_BE_D0_B6_D0_BA_D0_B0_Bloodborne_zsqu5h.jpg", "Bloodborne", 1, 30m, 16, 6.81m },
                    { new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1060, "RolePlaying", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229203/ASP_Labs/Among_Us_y6hnjs.png", "Among Us", 4, 40m, 6, 8.32m },
                    { new Guid("ea803243-ed41-49e9-9670-29619e3e4961"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1050, "MMO", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229211/ASP_Labs/Brawl_Stars_w86qfv.png", "Brawl Stars", 4, 50m, 12, 10m },
                    { new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1040, "Action", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229220/ASP_Labs/1920px-FortniteLogo.svg_kcwnbv.png", "Fortnite", 2, 60m, 6, 8.88m },
                    { new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1030, "Simulation", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229230/ASP_Labs/1920px-MinecraftLogo.svg_gamutf.png", "Minecraft", 2, 70m, 6, 9.51m },
                    { new Guid("50973345-c933-4098-9513-3c16d82dcc0a"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1020, "Action", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229084/ASP_Labs/Forza_Horizon_4_coverart_qmuz6l.jpg", "Forza Horizon 4", 2, 80m, 6, 9.49m },
                    { new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1010, "Action", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229253/ASP_Labs/Super_Smash_Bros._Ultimate_yvp3wa.png", "Super Smash Bros. Ultimate", 5, 90m, 6, 8.36m },
                    { new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 100, "Strategy", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229266/ASP_Labs/Super_Mario_Odyssey_box_ydaxqk.jpg", "Super Mario Odyssey", 5, 100m, 0, 8.43m },
                    { new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1600, "Adventure", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229280/ASP_Labs/Animal_Crossing_Logo_tbw4bw.png", "Animal Crossing", 5, 110m, 0, 7.55m },
                    { new Guid("1952c825-184a-40e9-8864-80358a9f1da6"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1500, "Strategy", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229295/ASP_Labs/D0_9E_D0_B1_D0_BB_D0_BE_D0_B6_D0_BA_D0_B0_Dota_2_rd8cox.jpg", "Dota 2", 3, 120m, 12, 7.64m },
                    { new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1400, "MMO", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229311/ASP_Labs/1920px-_D0_9B_D0_BE_D0_B3_D0_BE_D1_82_D0_B8_D0_BF_Counter-Strike_Global_Offensive.svg_cg9fcb.png", "CS GO", 3, 130m, 16, 8.27m },
                    { new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1300, "MMO", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229328/ASP_Labs/Overwatch_cover_art_whuqso.jpg", "Overwatch", 3, 140m, 12, 7.72m },
                    { new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1200, "Strategy", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229345/ASP_Labs/HL2box_lhx2ag.jpg", "Half-Life", 3, 150m, 16, 6.98m },
                    { new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1100, "Strategy", "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229364/ASP_Labs/Portal_boxcover_ojqdry.jpg", "Portal 2", 3, 160m, 0, 8.56m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1952c825-184a-40e9-8864-80358a9f1da6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("50973345-c933-4098-9513-3c16d82dcc0a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ea803243-ed41-49e9-9670-29619e3e4961"));

            migrationBuilder.AlterColumn<double>(
                name: "TotalRating",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,2)");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Background", "Count", "Genre", "Logo", "Name", "Platform", "Price", "Rating", "TotalRating" },
                values: new object[,]
                {
                    { new Guid("185a0529-c29f-40b5-8e20-134aab3f80be"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1090, "Esports", "https://ru.wikipedia.org/wiki/FIFA_(%D1%81%D0%B5%D1%80%D0%B8%D1%8F_%D0%B8%D0%B3%D1%80)#/media/%D0%A4%D0%B0%D0%B9%D0%BB:FIFA_series_logo.svg.png", "FIFA 2020", 1, 10m, 0, 7.3200000000000003 },
                    { new Guid("92671361-3c28-4e43-9175-3a6f31ef1ef1"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1080, "Strategy", "https://ru.wikipedia.org/wiki/God_of_War_(%D1%81%D0%B5%D1%80%D0%B8%D1%8F_%D0%B8%D0%B3%D1%80)#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9B%D0%BE%D0%B3%D0%BE%D1%82%D0%B8%D0%BF_%D0%B8%D0%B3%D1%80%D1%8B_God_of_War.png", "God of War", 1, 20m, 16, 8.3100000000000005 },
                    { new Guid("a9a3a4db-3947-4a77-a8b6-27571ac1d3a1"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1070, "Action", "https://ru.wikipedia.org/wiki/Bloodborne#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9E%D0%B1%D0%BB%D0%BE%D0%B6%D0%BA%D0%B0_Bloodborne.jpg", "Bloodborne", 1, 30m, 16, 6.8099999999999996 },
                    { new Guid("4857911c-cf2f-4803-87f0-bd99437eeeb8"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1060, "RolePlaying", "https://ru.wikipedia.org/wiki/Among_Us#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Among_Us.png", "Among Us", 4, 40m, 6, 8.3200000000000003 },
                    { new Guid("02811f45-9240-4f3d-a13f-361c0ca693e8"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1050, "MMO", "https://ru.wikipedia.org/wiki/Brawl_Stars#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Brawl_Stars.png", "Brawl Stars", 4, 50m, 12, 10.0 },
                    { new Guid("ae9993ac-2baa-4445-85a4-70097a1776cd"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1040, "Action", "https://ru.wikipedia.org/wiki/Fortnite#/media/%D0%A4%D0%B0%D0%B9%D0%BB:FortniteLogo.svg", "Fortnite", 2, 60m, 6, 8.8800000000000008 },
                    { new Guid("ff8d5672-43f3-47e8-afa6-673e2ba5759e"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1030, "Simulation", "https://ru.wikipedia.org/wiki/Minecraft#/media/%D0%A4%D0%B0%D0%B9%D0%BB:MinecraftLogo.svg", "Minecraft", 2, 70m, 6, 9.5099999999999998 },
                    { new Guid("1b27708f-f9da-4a83-9d54-041cd2889101"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1020, "Action", "https://ru.wikipedia.org/wiki/Forza_Horizon_4#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Forza_Horizon_4_coverart.jpg", "Forza Horizon 4", 2, 80m, 6, 9.4900000000000002 },
                    { new Guid("4d393685-ac1f-4fd4-b6a1-f5b6f8df39de"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1010, "Action", "https://ru.wikipedia.org/wiki/Super_Smash_Bros._Ultimate#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Super_Smash_Bros._Ultimate.png", "Super Smash Bros. Ultimate", 5, 90m, 6, 8.3599999999999994 },
                    { new Guid("789e46b7-6d01-40dc-9ad6-0469208097ba"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 100, "Strategy", "https://ru.wikipedia.org/wiki/Super_Mario_Odyssey#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Super_Mario_Odyssey_box.jpg", "Super Mario Odyssey", 5, 100m, 0, 8.4299999999999997 },
                    { new Guid("f092c4d6-5e3c-4860-a8cb-c8e14eae5e8d"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1600, "Adventure", "https://ru.wikipedia.org/wiki/Animal_Crossing#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Animal_Crossing_Logo.png", "Animal Crossing", 5, 110m, 0, 7.5499999999999998 },
                    { new Guid("70bd5995-fe29-4b93-8425-5a12e961c68b"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1500, "Strategy", "https://ru.wikipedia.org/wiki/Dota_2#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9E%D0%B1%D0%BB%D0%BE%D0%B6%D0%BA%D0%B0_Dota_2.jpg", "Dota 2", 3, 120m, 12, 7.6399999999999997 },
                    { new Guid("52b2bbf2-5e51-4789-9ddf-bc6d39f33b73"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1400, "MMO", "https://ru.wikipedia.org/wiki/Counter-Strike:_Global_Offensive#/media/%D0%A4%D0%B0%D0%B9%D0%BB:%D0%9B%D0%BE%D0%B3%D0%BE%D1%82%D0%B8%D0%BF_Counter-Strike_Global_Offensive.svg", "CS GO", 3, 130m, 16, 8.2699999999999996 },
                    { new Guid("832ff5f9-06c2-406e-8d4c-87e83a6a4aec"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1300, "MMO", "https://en.wikipedia.org/wiki/Overwatch_(video_game)#/media/File:Overwatch_cover_art.jpg", "Overwatch", 3, 140m, 12, 7.7199999999999998 },
                    { new Guid("dccff4b0-23e8-479e-8091-cd1145a5cd36"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1200, "Strategy", "https://ru.wikipedia.org/wiki/Half-Life_2#/media/%D0%A4%D0%B0%D0%B9%D0%BB:HL2box.jpg", "Half-Life", 3, 150m, 16, 6.9800000000000004 },
                    { new Guid("3e2bb91e-fb78-467f-a553-447cbd149f36"), "https://img.rawpixel.com/s3fs-private/rawpixel_images/website_content/v462-n-130-textureidea_1.jpg?w=1300&dpr=1&fit=default&crop=default&q=80&vib=3&con=3&usm=15&bg=F4F4F3&ixlib=js-2.2.1&s=1ba69b5c4ae053e9c312677688c2c4a2", 1100, "Strategy", "https://ru.wikipedia.org/wiki/Portal#/media/%D0%A4%D0%B0%D0%B9%D0%BB:Portal_boxcover.jpg", "Portal 2", 3, 160m, 0, 8.5600000000000005 }
                });
        }
    }
}
