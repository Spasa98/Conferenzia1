using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class v666 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocena");

            migrationBuilder.DropTable(
                name: "PredavanjeSlusalac");

            migrationBuilder.DropColumn(
                name: "brojZainteresovanih",
                table: "Predavanje");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "Organizator",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "brojZainteresovanih",
                table: "Predavanje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "Organizator",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Ocena",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PredavacID = table.Column<int>(type: "int", nullable: true),
                    VrednostOcene = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocena", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ocena_Predavac_PredavacID",
                        column: x => x.PredavacID,
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

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_PredavacID",
                table: "Ocena",
                column: "PredavacID");

            migrationBuilder.CreateIndex(
                name: "IX_PredavanjeSlusalac_PredavanjeSlusalacID1",
                table: "PredavanjeSlusalac",
                column: "PredavanjeSlusalacID1");
        }
    }
}
