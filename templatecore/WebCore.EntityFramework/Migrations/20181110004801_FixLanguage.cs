
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class FixLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "SystemConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "LanguageDetails");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "AppMenus");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "SystemConfigs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "LanguageDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "AppMenus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "SystemConfigs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "LanguageDetails");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AppMenus");

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "SystemConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "Languages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "LanguageDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "AppMenus",
                nullable: true);
        }
    }
}
