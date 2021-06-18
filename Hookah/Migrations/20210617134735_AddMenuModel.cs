using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hookah.Migrations
{
    public partial class AddMenuModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FlavorsTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FlavorsText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FruitHeadTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FruitHeadText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
