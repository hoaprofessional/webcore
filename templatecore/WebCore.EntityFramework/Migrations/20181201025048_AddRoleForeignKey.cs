using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddRoleForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebCoreRoleId",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebCoreRoleClaims_WebCoreRoleId",
                table: "WebCoreRoleClaims",
                column: "WebCoreRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebCoreRoleClaims_WebCoreRoles_WebCoreRoleId",
                table: "WebCoreRoleClaims",
                column: "WebCoreRoleId",
                principalTable: "WebCoreRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebCoreRoleClaims_WebCoreRoles_WebCoreRoleId",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropIndex(
                name: "IX_WebCoreRoleClaims_WebCoreRoleId",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "WebCoreRoleId",
                table: "WebCoreRoleClaims");
        }
    }
}
