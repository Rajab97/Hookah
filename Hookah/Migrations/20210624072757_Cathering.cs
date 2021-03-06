using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hookah.Migrations
{
    public partial class Cathering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cathering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageLP = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImagePL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageMB = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BaseTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseTitleText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BaseTitleBoldText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SelectPackageTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HowItWorksTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cathering", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cathering");
        }
    }
}
