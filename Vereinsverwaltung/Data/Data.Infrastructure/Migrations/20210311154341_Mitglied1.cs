using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vereinsverwaltung.Data.Infrastructure.Migrations
{
    public partial class Mitglied1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AusgetretenAm",
                table: "Mitglied",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AusgetretenAm",
                table: "Mitglied");
        }
    }
}
