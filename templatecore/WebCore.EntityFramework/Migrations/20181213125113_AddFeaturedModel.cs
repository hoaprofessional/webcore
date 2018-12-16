using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddFeaturedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 256, nullable: true),
                    RecordStatus = table.Column<long>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    UpdateToken = table.Column<Guid>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Between10To20Character = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: true),
                    Beetween10To20 = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Money = table.Column<decimal>(nullable: true),
                    Combobox = table.Column<string>(nullable: true),
                    MultiSelect = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: true),
                    ToDate = table.Column<DateTime>(nullable: true),
                    LessThanToday = table.Column<DateTime>(nullable: true),
                    GreaterThanToday = table.Column<DateTime>(nullable: true),
                    TextArea = table.Column<string>(nullable: true),
                    TextEditor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
