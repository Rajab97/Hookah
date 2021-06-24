using Microsoft.EntityFrameworkCore.Migrations;

namespace Hookah.Migrations
{
    public partial class HomeLinEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "HomeLinks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "HomeLinks");
        }
    }
}
