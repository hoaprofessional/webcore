using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddAuditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WebCoreUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "WebCoreUsers",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "WebCoreUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WebCoreUserRoles",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "WebCoreUserRoles",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "WebCoreUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WebCoreUserLogins",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "WebCoreUserLogins",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "WebCoreUserLogins",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WebCoreUserClaims",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "WebCoreUserClaims",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "WebCoreUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WebCoreRoles",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "WebCoreRoles",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "WebCoreRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WebCoreRoleClaims",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "WebCoreRoleClaims",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "WebCoreRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AspNetUserTokens",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AspNetUserTokens",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecordStatus",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 256, nullable: true),
                    RecordStatus = table.Column<long>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    EmailPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "WebCoreUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "WebCoreUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "WebCoreUserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "WebCoreUserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "WebCoreRoles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "WebCoreRoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "AspNetUserTokens");
        }
    }
}
