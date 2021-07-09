using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApp.DAL.Migrations
{
    public partial class UpdateRolePlayingName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"),
                column: "Genre",
                value: "RolePlaying");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"),
                column: "Genre",
                value: "Role_Playing");
        }
    }
}
