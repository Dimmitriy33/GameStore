using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    TotalRating = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRating",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRating", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProductRating_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRating_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Background", "Count", "Genre", "IsDeleted", "Logo", "Name", "Platform", "Price", "Rating", "TotalRating" },
                values: new object[,]
                {
                    { new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1090, "Esports", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229539/ASP_Labs/FIFA_series_logo.svg_geizkx.png", "FIFA 2020", 1, 10m, 0, 7.32m },
                    { new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1080, "Strategy", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229190/ASP_Labs/D0_9B_D0_BE_D0_B3_D0_BE_D1_82_D0_B8_D0_BF__D0_B8_D0_B3_D1_80_D1_8B_God_of_War_mgno9l.png", "God of War", 1, 20m, 16, 8.31m },
                    { new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1070, "Action", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229196/ASP_Labs/D0_9E_D0_B1_D0_BB_D0_BE_D0_B6_D0_BA_D0_B0_Bloodborne_zsqu5h.jpg", "Bloodborne", 1, 30m, 16, 6.81m },
                    { new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1060, "RolePlaying", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229203/ASP_Labs/Among_Us_y6hnjs.png", "Among Us", 4, 40m, 6, 8.32m },
                    { new Guid("ea803243-ed41-49e9-9670-29619e3e4961"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1050, "MMO", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229211/ASP_Labs/Brawl_Stars_w86qfv.png", "Brawl Stars", 4, 50m, 12, 10m },
                    { new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1040, "Action", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229220/ASP_Labs/1920px-FortniteLogo.svg_kcwnbv.png", "Fortnite", 2, 60m, 6, 8.88m },
                    { new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1030, "Simulation", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229230/ASP_Labs/1920px-MinecraftLogo.svg_gamutf.png", "Minecraft", 2, 70m, 6, 9.51m },
                    { new Guid("50973345-c933-4098-9513-3c16d82dcc0a"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1020, "Action", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229084/ASP_Labs/Forza_Horizon_4_coverart_qmuz6l.jpg", "Forza Horizon 4", 2, 80m, 6, 9.49m },
                    { new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1010, "Action", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229253/ASP_Labs/Super_Smash_Bros._Ultimate_yvp3wa.png", "Super Smash Bros. Ultimate", 5, 90m, 6, 8.36m },
                    { new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 100, "Strategy", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229266/ASP_Labs/Super_Mario_Odyssey_box_ydaxqk.jpg", "Super Mario Odyssey", 5, 100m, 0, 8.43m },
                    { new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1600, "Adventure", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229280/ASP_Labs/Animal_Crossing_Logo_tbw4bw.png", "Animal Crossing", 5, 110m, 0, 7.55m },
                    { new Guid("1952c825-184a-40e9-8864-80358a9f1da6"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1500, "Strategy", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229295/ASP_Labs/D0_9E_D0_B1_D0_BB_D0_BE_D0_B6_D0_BA_D0_B0_Dota_2_rd8cox.jpg", "Dota 2", 3, 120m, 12, 7.64m },
                    { new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1400, "MMO", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229311/ASP_Labs/1920px-_D0_9B_D0_BE_D0_B3_D0_BE_D1_82_D0_B8_D0_BF_Counter-Strike_Global_Offensive.svg_cg9fcb.png", "CS GO", 3, 130m, 16, 8.27m },
                    { new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1300, "MMO", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229328/ASP_Labs/Overwatch_cover_art_whuqso.jpg", "Overwatch", 3, 140m, 12, 7.72m },
                    { new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1200, "Strategy", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229345/ASP_Labs/HL2box_lhx2ag.jpg", "Half-Life", 3, 150m, 16, 6.98m },
                    { new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"), "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229442/ASP_Labs/v462-n-130-textureidea_1.jpg_ghjewf.jpg", 1100, "Strategy", false, "https://res.cloudinary.com/dimmitriy33/image/upload/v1625229364/ASP_Labs/Portal_boxcover_ojqdry.jpg", "Portal 2", 3, 160m, 0, 8.56m }
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
                name: "IX_Orders_OrderId",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_Rating",
                table: "ProductRating",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRating_UserId",
                table: "ProductRating",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Count",
                table: "Products",
                column: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateCreated",
                table: "Products",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Genre",
                table: "Products",
                column: "Genre");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Platform",
                table: "Products",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Price",
                table: "Products",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Rating",
                table: "Products",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TotalRating",
                table: "Products",
                column: "TotalRating");
        }

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
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductRating");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
