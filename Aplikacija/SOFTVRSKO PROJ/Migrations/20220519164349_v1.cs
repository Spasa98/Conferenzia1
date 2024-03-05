using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oblast",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oblast", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Organizator",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizator", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Slusalac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<int>(type: "int", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchiveFlag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slusalac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zvanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zvanje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Predavac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<int>(type: "int", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    PutanjaSlike = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchiveFlag = table.Column<int>(type: "int", nullable: false),
                    ZvanjeID = table.Column<int>(type: "int", nullable: true),
                    OblastID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predavac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Predavac_Oblast_OblastID",
                        column: x => x.OblastID,
                        principalTable: "Oblast",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predavac_Zvanje_ZvanjeID",
                        column: x => x.ZvanjeID,
                        principalTable: "Zvanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "date", nullable: false),
                    Tip = table.Column<int>(type: "int", nullable: false),
                    KometarisaniPredavacID = table.Column<int>(type: "int", nullable: true),
                    SlusalacKomentariseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Feedback_Predavac_KometarisaniPredavacID",
                        column: x => x.KometarisaniPredavacID,
                        principalTable: "Predavac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedback_Slusalac_SlusalacKomentariseID",
                        column: x => x.SlusalacKomentariseID,
                        principalTable: "Slusalac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Predavanje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kapacitet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OblastID = table.Column<int>(type: "int", nullable: true),
                    PredavacID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predavanje", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Predavanje_Oblast_OblastID",
                        column: x => x.OblastID,
                        principalTable: "Oblast",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predavanje_Predavac_PredavacID",
                        column: x => x.PredavacID,
                        principalTable: "Predavac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportFeedback",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KomentarID = table.Column<int>(type: "int", nullable: true),
                    FeedbackID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportFeedback", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportFeedback_Feedback_FeedbackID",
                        column: x => x.FeedbackID,
                        principalTable: "Feedback",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportFeedback_Predavac_KomentarID",
                        column: x => x.KomentarID,
                        principalTable: "Predavac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PredavanjeSlusalac",
                columns: table => new
                {
                    PredavanjeSlusalacID = table.Column<int>(type: "int", nullable: false),
                    PredavanjeSlusalacID1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredavanjeSlusalac", x => new { x.PredavanjeSlusalacID, x.PredavanjeSlusalacID1 });
                    table.ForeignKey(
                        name: "FK_PredavanjeSlusalac_Predavanje_PredavanjeSlusalacID",
                        column: x => x.PredavanjeSlusalacID,
                        principalTable: "Predavanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PredavanjeSlusalac_Slusalac_PredavanjeSlusalacID1",
                        column: x => x.PredavanjeSlusalacID1,
                        principalTable: "Slusalac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZahtevPredavac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredavacID = table.Column<int>(type: "int", nullable: true),
                    PredavanjeID = table.Column<int>(type: "int", nullable: true),
                    OrganizatorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtevPredavac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZahtevPredavac_Organizator_OrganizatorID",
                        column: x => x.OrganizatorID,
                        principalTable: "Organizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZahtevPredavac_Predavac_PredavacID",
                        column: x => x.PredavacID,
                        principalTable: "Predavac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZahtevPredavac_Predavanje_PredavanjeID",
                        column: x => x.PredavanjeID,
                        principalTable: "Predavanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZahtevSlusalac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlusalacID = table.Column<int>(type: "int", nullable: true),
                    PredavanjeID = table.Column<int>(type: "int", nullable: true),
                    OrganizatorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtevSlusalac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ZahtevSlusalac_Organizator_OrganizatorID",
                        column: x => x.OrganizatorID,
                        principalTable: "Organizator",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZahtevSlusalac_Predavanje_PredavanjeID",
                        column: x => x.PredavanjeID,
                        principalTable: "Predavanje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ZahtevSlusalac_Slusalac_SlusalacID",
                        column: x => x.SlusalacID,
                        principalTable: "Slusalac",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_KometarisaniPredavacID",
                table: "Feedback",
                column: "KometarisaniPredavacID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_SlusalacKomentariseID",
                table: "Feedback",
                column: "SlusalacKomentariseID");

            migrationBuilder.CreateIndex(
                name: "IX_Predavac_OblastID",
                table: "Predavac",
                column: "OblastID");

            migrationBuilder.CreateIndex(
                name: "IX_Predavac_ZvanjeID",
                table: "Predavac",
                column: "ZvanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Predavanje_OblastID",
                table: "Predavanje",
                column: "OblastID");

            migrationBuilder.CreateIndex(
                name: "IX_Predavanje_PredavacID",
                table: "Predavanje",
                column: "PredavacID");

            migrationBuilder.CreateIndex(
                name: "IX_PredavanjeSlusalac_PredavanjeSlusalacID1",
                table: "PredavanjeSlusalac",
                column: "PredavanjeSlusalacID1");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFeedback_FeedbackID",
                table: "ReportFeedback",
                column: "FeedbackID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportFeedback_KomentarID",
                table: "ReportFeedback",
                column: "KomentarID");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevPredavac_OrganizatorID",
                table: "ZahtevPredavac",
                column: "OrganizatorID");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevPredavac_PredavacID",
                table: "ZahtevPredavac",
                column: "PredavacID");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevPredavac_PredavanjeID",
                table: "ZahtevPredavac",
                column: "PredavanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevSlusalac_OrganizatorID",
                table: "ZahtevSlusalac",
                column: "OrganizatorID");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevSlusalac_PredavanjeID",
                table: "ZahtevSlusalac",
                column: "PredavanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_ZahtevSlusalac_SlusalacID",
                table: "ZahtevSlusalac",
                column: "SlusalacID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredavanjeSlusalac");

            migrationBuilder.DropTable(
                name: "ReportFeedback");

            migrationBuilder.DropTable(
                name: "ZahtevPredavac");

            migrationBuilder.DropTable(
                name: "ZahtevSlusalac");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Organizator");

            migrationBuilder.DropTable(
                name: "Predavanje");

            migrationBuilder.DropTable(
                name: "Slusalac");

            migrationBuilder.DropTable(
                name: "Predavac");

            migrationBuilder.DropTable(
                name: "Oblast");

            migrationBuilder.DropTable(
                name: "Zvanje");
        }
    }
}
