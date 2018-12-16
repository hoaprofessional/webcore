using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class EditFeatured : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "Minutes",
                table: "Features",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "DateWithHour",
                table: "Features",
                newName: "FullDateAndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Features",
                newName: "Minutes");

            migrationBuilder.RenameColumn(
                name: "FullDateAndTime",
                table: "Features",
                newName: "DateWithHour");

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Features",
                nullable: false,
                defaultValue: 0);
        }
    }
}
