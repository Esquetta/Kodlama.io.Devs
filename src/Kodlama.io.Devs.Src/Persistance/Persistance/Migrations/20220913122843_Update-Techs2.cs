using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class UpdateTechs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GithubAccounts_GithubAccountId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GithubAccounts_GithubAccountId",
                table: "User",
                column: "GithubAccountId",
                principalTable: "GithubAccounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_GithubAccounts_GithubAccountId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_GithubAccounts_GithubAccountId",
                table: "User",
                column: "GithubAccountId",
                principalTable: "GithubAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
