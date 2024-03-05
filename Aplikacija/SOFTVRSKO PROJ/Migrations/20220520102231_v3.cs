using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kapacitet",
                table: "Predavanje");

            migrationBuilder.AddColumn<int>(
                name: "salaID",
                table: "Predavanje",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Predavanje_salaID",
                table: "Predavanje",
                column: "salaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Predavanje_Sala_salaID",
                table: "Predavanje",
                column: "salaID",
                principalTable: "Sala",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predavanje_Sala_salaID",
                table: "Predavanje");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropIndex(
                name: "IX_Predavanje_salaID",
                table: "Predavanje");

            migrationBuilder.DropColumn(
                name: "salaID",
                table: "Predavanje");

            migrationBuilder.AddColumn<int>(
                name: "Kapacitet",
                table: "Predavanje",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
