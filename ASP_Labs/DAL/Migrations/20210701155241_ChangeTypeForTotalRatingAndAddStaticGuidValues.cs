using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApp.DAL.Migrations
{
    public partial class ChangeTypeForTotalRatingAndAddStaticGuidValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalRating",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"),
                column: "TotalRating",
                value: 6.9800000000000004);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"),
                column: "TotalRating",
                value: 9.5099999999999998);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1952c825-184a-40e9-8864-80358a9f1da6"),
                column: "TotalRating",
                value: 7.6399999999999997);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"),
                column: "TotalRating",
                value: 8.5600000000000005);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("50973345-c933-4098-9513-3c16d82dcc0a"),
                column: "TotalRating",
                value: 9.4900000000000002);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"),
                column: "TotalRating",
                value: 8.3200000000000003);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"),
                column: "TotalRating",
                value: 6.8099999999999996);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"),
                column: "TotalRating",
                value: 8.8800000000000008);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"),
                column: "TotalRating",
                value: 8.3100000000000005);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"),
                column: "TotalRating",
                value: 8.4299999999999997);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"),
                column: "TotalRating",
                value: 7.5499999999999998);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"),
                column: "TotalRating",
                value: 7.3200000000000003);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"),
                column: "TotalRating",
                value: 7.7199999999999998);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"),
                column: "TotalRating",
                value: 8.3599999999999994);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"),
                column: "TotalRating",
                value: 8.2699999999999996);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ea803243-ed41-49e9-9670-29619e3e4961"),
                column: "TotalRating",
                value: 10.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalRating",
                table: "Products",
                type: "decimal(2,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("00189d6e-ed62-482b-a4d9-335dfa68d58e"),
                column: "TotalRating",
                value: 6.98m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0e70d082-9558-48aa-84a8-5a34ac95af08"),
                column: "TotalRating",
                value: 9.51m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1952c825-184a-40e9-8864-80358a9f1da6"),
                column: "TotalRating",
                value: 7.64m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("1ad798c4-da8c-4e87-a020-9272e4e71d2b"),
                column: "TotalRating",
                value: 8.56m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("50973345-c933-4098-9513-3c16d82dcc0a"),
                column: "TotalRating",
                value: 9.49m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("5acb3515-9b43-4b80-9338-1ff4e0dc972b"),
                column: "TotalRating",
                value: 8.32m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("67550e04-f55d-40c6-bd72-3cbffef51317"),
                column: "TotalRating",
                value: 6.81m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("77133fcf-3da2-42d0-9b9d-32ec0dc5f421"),
                column: "TotalRating",
                value: 8.88m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("7bad0c87-edd2-4a23-aade-aaff2e19f54f"),
                column: "TotalRating",
                value: 8.31m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("82e27206-1bfd-4d62-a6bf-be44ad030b25"),
                column: "TotalRating",
                value: 8.43m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a1c40a91-a1a4-4d9e-960d-b0c19b425c8c"),
                column: "TotalRating",
                value: 7.55m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a76d6bde-c48c-4dcb-b80a-7c6edce28c74"),
                column: "TotalRating",
                value: 7.32m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("c3279ca3-8fe4-45c9-8606-ae09f8b7f259"),
                column: "TotalRating",
                value: 7.72m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("cd4e1a11-ef0c-402c-a2ab-b18622ea1eb9"),
                column: "TotalRating",
                value: 8.36m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d4b797a8-2f74-446e-ad0c-71dad9e37e59"),
                column: "TotalRating",
                value: 8.27m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ea803243-ed41-49e9-9670-29619e3e4961"),
                column: "TotalRating",
                value: 10m);
        }
    }
}
