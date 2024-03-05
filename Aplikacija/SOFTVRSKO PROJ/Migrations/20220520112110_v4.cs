using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocena",
                table: "Predavac");

            migrationBuilder.AlterColumn<string>(
                name: "PutanjaSlike",
                table: "Predavac",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Ocena",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrednostOcene = table.Column<int>(type: "int", nullable: false),
                    PredavacID = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Ocena_PredavacID",
                table: "Ocena",
                column: "PredavacID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocena");

            migrationBuilder.AlterColumn<string>(
                name: "PutanjaSlike",
                table: "Predavac",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ocena",
                table: "Predavac",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
