using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hookah.Migrations
{
    public partial class SiteConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteConfiguration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstagramLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    YoutubeLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FacebookLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PhoneNumberForCall = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CallButtonText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestCallButton = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InstagramProfileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyLogoImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteConfiguration", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteConfiguration");
        }
    }
}
