using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vereinsverwaltung.Data.Infrastructure.Migrations
{
    public partial class Schnur1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schnur",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bezeichnung = table.Column<string>(nullable: true),
                    Schnurtyp = table.Column<int>(nullable: false),
                    Sichtbar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schnur", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Schnurauszeichnung",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bezeichnung = table.Column<string>(nullable: true),
                    Rangfolge = table.Column<int>(nullable: false),
                    HauptteilID = table.Column<int>(nullable: false),
                    ZusatzID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schnurauszeichnung", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schnurauszeichnung_Schnur_HauptteilID",
                        column: x => x.HauptteilID,
                        principalTable: "Schnur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schnurauszeichnung_Schnur_ZusatzID",
                        column: x => x.ZusatzID,
                        principalTable: "Schnur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schnurauszeichnung_HauptteilID",
                table: "Schnurauszeichnung",
                column: "HauptteilID");

            migrationBuilder.CreateIndex(
                name: "IX_Schnurauszeichnung_ZusatzID",
                table: "Schnurauszeichnung",
                column: "ZusatzID");

            migrationBuilder.InsertData("Schnur", columns: new[] { "Bezeichnung", "Sichtbar", "Schnurtyp" } , values: new object[]
            
                { "Kein Zusatz" , false , "1" }  );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schnurauszeichnung");

            migrationBuilder.DropTable(
                name: "Schnur");
        }
    }
}
