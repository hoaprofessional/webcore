using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class UpdateFeatured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultiFile",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultiImage",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoValidateText",
                table: "Features",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "MultiFile",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "MultiImage",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "NoValidateText",
                table: "Features");
        }
    }
}
