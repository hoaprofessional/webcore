using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class FixAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "SystemConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "SystemConfigs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "SystemConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "LanguageDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "LanguageDetails",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "LanguageDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "AppMenus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "AppMenus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateToken",
                table: "AppMenus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "SystemConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "SystemConfigs");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "SystemConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LanguageDetails");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "LanguageDetails");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "LanguageDetails");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "AppMenus");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "AppMenus");

            migrationBuilder.DropColumn(
                name: "UpdateToken",
                table: "AppMenus");
        }
    }
}
