using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vereinsverwaltung.Data.Infrastructure.Migrations
{
    public partial class Schluesselliste2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchluesselzuteilungHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchluesselID = table.Column<int>(nullable: false),
                    SchluesselbesitzerID = table.Column<int>(nullable: false),
                    SchluesselzuteilungID = table.Column<int>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Anzahl = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchluesselzuteilungHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SchluesselzuteilungHistory_Schluessel_SchluesselID",
                        column: x => x.SchluesselID,
                        principalTable: "Schluessel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchluesselzuteilungHistory_Schluesselbesitzer_Schluesselbes~",
                        column: x => x.SchluesselbesitzerID,
                        principalTable: "Schluesselbesitzer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchluesselzuteilungHistory_Schluesselzuteilung_Schluesselzu~",
                        column: x => x.SchluesselzuteilungID,
                        principalTable: "Schluesselzuteilung",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchluesselzuteilungHistory_SchluesselID",
                table: "SchluesselzuteilungHistory",
                column: "SchluesselID");

            migrationBuilder.CreateIndex(
                name: "IX_SchluesselzuteilungHistory_SchluesselbesitzerID",
                table: "SchluesselzuteilungHistory",
                column: "SchluesselbesitzerID");

            migrationBuilder.CreateIndex(
                name: "IX_SchluesselzuteilungHistory_SchluesselzuteilungID",
                table: "SchluesselzuteilungHistory",
                column: "SchluesselzuteilungID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchluesselzuteilungHistory");
        }
    }
}
