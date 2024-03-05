using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class v56 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predavac_Oblast_OblastID",
                table: "Predavac");

            migrationBuilder.DropForeignKey(
                name: "FK_Predavac_Zvanje_ZvanjeID",
                table: "Predavac");

            migrationBuilder.AlterColumn<int>(
                name: "ZvanjeID",
                table: "Predavac",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OblastID",
                table: "Predavac",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Predavac_Oblast_OblastID",
                table: "Predavac",
                column: "OblastID",
                principalTable: "Oblast",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Predavac_Zvanje_ZvanjeID",
                table: "Predavac",
                column: "ZvanjeID",
                principalTable: "Zvanje",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predavac_Oblast_OblastID",
                table: "Predavac");

            migrationBuilder.DropForeignKey(
                name: "FK_Predavac_Zvanje_ZvanjeID",
                table: "Predavac");

            migrationBuilder.AlterColumn<int>(
                name: "ZvanjeID",
                table: "Predavac",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OblastID",
                table: "Predavac",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Predavac_Oblast_OblastID",
                table: "Predavac",
                column: "OblastID",
                principalTable: "Oblast",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Predavac_Zvanje_ZvanjeID",
                table: "Predavac",
                column: "ZvanjeID",
                principalTable: "Zvanje",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
