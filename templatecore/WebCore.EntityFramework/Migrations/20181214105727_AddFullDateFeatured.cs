using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddFullDateFeatured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateWithHour",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Features",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                table: "Features",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateWithHour",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "Features");
        }
    }
}
