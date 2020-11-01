using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlPersonalizada",
                table: "Artigos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_UrlPersonalizada",
                table: "Artigos",
                column: "UrlPersonalizada",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Artigos_UrlPersonalizada",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "UrlPersonalizada",
                table: "Artigos");
        }
    }
}
