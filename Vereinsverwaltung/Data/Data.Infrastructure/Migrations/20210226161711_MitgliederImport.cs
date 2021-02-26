using Microsoft.EntityFrameworkCore.Migrations;

namespace Vereinsverwaltung.Data.Infrastructure.Migrations
{
    public partial class MitgliederImport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MitgliedStatus",
                table: "Mitglied",
                nullable: false,
                defaultValue: 0              
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MitgliedStatus",
                table: "Mitglied");
        }
    }
}
