using Microsoft.EntityFrameworkCore.Migrations;

namespace Hookah.Migrations
{
    public partial class IsContacted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsContacted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContacted",
                table: "Orders");
        }
    }
}
