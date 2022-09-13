using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class AddTechs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tech",
                columns: new[] { "Id", "LanguageId", "Name" },
                values: new object[] { 1, 2, "WPF" });

            migrationBuilder.InsertData(
                table: "Tech",
                columns: new[] { "Id", "LanguageId", "Name" },
                values: new object[] { 2, 2, "ASP.NET" });

            migrationBuilder.InsertData(
                table: "Tech",
                columns: new[] { "Id", "LanguageId", "Name" },
                values: new object[] { 3, 3, "Spring" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tech",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
