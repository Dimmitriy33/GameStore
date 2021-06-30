using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApp.DAL.Migrations
{
    public partial class Addproductswithdefaultdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    TotalRating = table.Column<decimal>(type: "decimal(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Name", "Platform", "TotalRating" },
                values: new object[,]
                {
                    { new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"), "FIFA 2020", 1, 7.32m },
                    { new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"), "God of War", 1, 8.31m },
                    { new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"), "Bloodborne", 1, 6.81m },
                    { new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"), "Among Us", 4, 8.32m },
                    { new Guid("ea803243-ed41-49e9-9670-29619e3e4961"), "Brawl Stars", 4, 10m },
                    { new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"), "Fortnite", 2, 8.88m },
                    { new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"), "Minecraft", 2, 9.51m },
                    { new Guid("50973345-c933-4098-9513-3c16d82dcc0a"), "Forza Horizon 4", 2, 9.49m },
                    { new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"), "Super Smash Bros. Ultimate", 5, 8.36m },
                    { new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"), "Super Mario Odyssey", 5, 8.43m },
                    { new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"), "Animal Crossing", 5, 7.55m },
                    { new Guid("1952c825-184a-40e9-8864-80358a9f1da6"), "Dota 2", 3, 7.64m },
                    { new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"), "CS GO", 3, 8.27m },
                    { new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"), "Overwatch", 3, 7.72m },
                    { new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"), "Half-Life", 3, 6.98m },
                    { new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"), "Portal 2", 3, 8.56m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DateCreated",
                table: "Products",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Platform",
                table: "Products",
                column: "Platform");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TotalRating",
                table: "Products",
                column: "TotalRating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
