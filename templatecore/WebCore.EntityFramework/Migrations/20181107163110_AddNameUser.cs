using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddNameUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "WebCoreUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "WebCoreUsers");
        }
    }
}
