using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vereinsverwaltung.Data.Infrastructure.Migrations
{
    public partial class Schluesselliste1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schluessel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bezeichnung = table.Column<string>(nullable: true),
                    Beschreibung = table.Column<string>(nullable: true),
                    Nummer = table.Column<int>(nullable: false),
                    Bestand = table.Column<int>(nullable: false),
                    Ausgegeben = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schluessel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Schluesselbesitzer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MitgliedID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schluesselbesitzer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schluesselbesitzer_Mitglied_MitgliedID",
                        column: x => x.MitgliedID,
                        principalTable: "Mitglied",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schluesselzuteilung",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchluesselID = table.Column<int>(nullable: false),
                    SchluesselbesitzerID = table.Column<int>(nullable: false),
                    Anzahl = table.Column<int>(nullable: false),
                    ErhaltenAm = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schluesselzuteilung", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schluesselzuteilung_Schluessel_SchluesselID",
                        column: x => x.SchluesselID,
                        principalTable: "Schluessel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schluesselzuteilung_Schluesselbesitzer_SchluesselbesitzerID",
                        column: x => x.SchluesselbesitzerID,
                        principalTable: "Schluesselbesitzer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schluesselbesitzer_MitgliedID",
                table: "Schluesselbesitzer",
                column: "MitgliedID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schluesselzuteilung_SchluesselID",
                table: "Schluesselzuteilung",
                column: "SchluesselID");

            migrationBuilder.CreateIndex(
                name: "IX_Schluesselzuteilung_SchluesselbesitzerID",
                table: "Schluesselzuteilung",
                column: "SchluesselbesitzerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schluesselzuteilung");

            migrationBuilder.DropTable(
                name: "Schluessel");

            migrationBuilder.DropTable(
                name: "Schluesselbesitzer");
        }
    }
}
