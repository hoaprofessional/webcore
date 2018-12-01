using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddRoleForeignKey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebCoreRoleClaims_WebCoreRoles_RoleId1",
                table: "WebCoreRoleClaims");

            migrationBuilder.RenameColumn(
                name: "RoleId1",
                table: "WebCoreRoleClaims",
                newName: "WebCoreRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_WebCoreRoleClaims_RoleId1",
                table: "WebCoreRoleClaims",
                newName: "IX_WebCoreRoleClaims_WebCoreRoleId");

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

            migrationBuilder.RenameColumn(
                name: "WebCoreRoleId",
                table: "WebCoreRoleClaims",
                newName: "RoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_WebCoreRoleClaims_WebCoreRoleId",
                table: "WebCoreRoleClaims",
                newName: "IX_WebCoreRoleClaims_RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WebCoreRoleClaims_WebCoreRoles_RoleId1",
                table: "WebCoreRoleClaims",
                column: "RoleId1",
                principalTable: "WebCoreRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
