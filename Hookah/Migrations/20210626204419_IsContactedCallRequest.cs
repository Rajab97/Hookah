using Microsoft.EntityFrameworkCore.Migrations;

namespace Hookah.Migrations
{
    public partial class IsContactedCallRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsContacted",
                table: "CallRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContacted",
                table: "CallRequests");
        }
    }
}
