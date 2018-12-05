using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddMasterListGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "MasterLists");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "MasterLists",
                newName: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "OrderNo",
                table: "MasterLists",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Group",
                table: "MasterLists",
                newName: "Key");

            migrationBuilder.AlterColumn<int>(
                name: "OrderNo",
                table: "MasterLists",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "MasterLists",
                nullable: true);
        }
    }
}
