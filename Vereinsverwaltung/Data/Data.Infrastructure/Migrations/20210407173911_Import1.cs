using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vereinsverwaltung.Data.Infrastructure.Migrations
{
    public partial class Import1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Geschlecht",
                table: "Mitglied",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MitgliedImportHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EingelesenAm = table.Column<DateTime>(nullable: false),
                    GesamtEingelesen = table.Column<int>(nullable: false),
                    AnzahlNeu = table.Column<int>(nullable: false),
                    AnzahlEhemalig = table.Column<int>(nullable: false),
                    AnzahlKeineAenderung = table.Column<int>(nullable: false),
                    AnzahlAenderung = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MitgliedImportHistory", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MitgliedImportHistory");

            migrationBuilder.DropColumn(
                name: "Geschlecht",
                table: "Mitglied");
        }
    }
}
