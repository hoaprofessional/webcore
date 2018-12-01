using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class FixSystemConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailPassword",
                table: "SystemConfigs",
                newName: "ValueString");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "SystemConfigs",
                newName: "Key");

            migrationBuilder.AddColumn<decimal>(
                name: "ValueNumber",
                table: "SystemConfigs",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueNumber",
                table: "SystemConfigs");

            migrationBuilder.RenameColumn(
                name: "ValueString",
                table: "SystemConfigs",
                newName: "EmailPassword");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "SystemConfigs",
                newName: "EmailAddress");
        }
    }
}
