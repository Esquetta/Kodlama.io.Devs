using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class AddTechs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 1,
                column: "LanguageId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 2,
                column: "LanguageId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 3,
                column: "LanguageId",
                value: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 1,
                column: "LanguageId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 2,
                column: "LanguageId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 3,
                column: "LanguageId",
                value: 3);
        }
    }
}
