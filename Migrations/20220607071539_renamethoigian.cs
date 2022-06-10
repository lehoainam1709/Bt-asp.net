using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetShop.Migrations
{
    public partial class renamethoigian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayMua",
                table: "Pet");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gia",
                table: "Pet",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGian",
                table: "Pet",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiGian",
                table: "Pet");

            migrationBuilder.AlterColumn<decimal>(
                name: "Gia",
                table: "Pet",
                type: "decimal(18,2",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayMua",
                table: "Pet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
