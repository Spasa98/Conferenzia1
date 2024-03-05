using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOFTVRSKO_PROJ.Migrations
{
    public partial class ZahtevPredavacMigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                table: "ZahtevPredavac",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Datum",
                table: "ZahtevPredavac",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
