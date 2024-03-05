using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class vPromenaPredavacZahtev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PrijavaOdjava",
                table: "ZahtevPredavac",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PrijavaOdjava",
                table: "ZahtevPredavac",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
