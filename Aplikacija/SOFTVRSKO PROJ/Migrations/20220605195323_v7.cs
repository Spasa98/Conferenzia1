using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "ZahtevPredavac");

            migrationBuilder.AddColumn<bool>(
                name: "PrijavaOdjava",
                table: "ZahtevPredavac",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrijavaOdjava",
                table: "ZahtevPredavac");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "ZahtevPredavac",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
