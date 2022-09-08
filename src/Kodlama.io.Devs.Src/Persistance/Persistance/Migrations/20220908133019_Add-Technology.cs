using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class AddTechnology : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tech",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tech", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tech_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Tech_LanguageId",
                table: "Tech",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tech");
        }
    }
}
