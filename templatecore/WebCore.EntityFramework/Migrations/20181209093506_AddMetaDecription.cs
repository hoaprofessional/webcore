using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.EntityFramework.Migrations
{
    public partial class AddMetaDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "MetaDescriptions",
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
                    Alias = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaDescriptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 256, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    RecordStatus = table.Column<long>(nullable: true),
                    ShortDecription = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UpdateToken = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });
        }
    }
}
