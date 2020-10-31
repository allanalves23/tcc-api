using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Autores");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Autores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Autores");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Autores",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Autores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
