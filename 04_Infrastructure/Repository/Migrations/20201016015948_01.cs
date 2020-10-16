using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Artigos",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Artigos",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Artigos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Artigos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Artigos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Artigos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Artigos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataImpulsionamento",
                table: "Artigos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInativacao",
                table: "Artigos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPublicacao",
                table: "Artigos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRemocao",
                table: "Artigos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Artigos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TemaId",
                table: "Artigos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Senha = table.Column<string>(maxLength: 255, nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Genero = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    DataRemocao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    DataRemocao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(maxLength: 255, nullable: true),
                    TemaId = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DataAtualizacao = table.Column<DateTime>(nullable: true),
                    DataRemocao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Temas_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Temas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_AutorId",
                table: "Artigos",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_CategoriaId",
                table: "Artigos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artigos_TemaId",
                table: "Artigos",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_TemaId",
                table: "Categorias",
                column: "TemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artigos_Autores_AutorId",
                table: "Artigos",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Artigos_Categorias_CategoriaId",
                table: "Artigos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Artigos_Temas_TemaId",
                table: "Artigos",
                column: "TemaId",
                principalTable: "Temas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artigos_Autores_AutorId",
                table: "Artigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Artigos_Categorias_CategoriaId",
                table: "Artigos");

            migrationBuilder.DropForeignKey(
                name: "FK_Artigos_Temas_TemaId",
                table: "Artigos");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Temas");

            migrationBuilder.DropIndex(
                name: "IX_Artigos_AutorId",
                table: "Artigos");

            migrationBuilder.DropIndex(
                name: "IX_Artigos_CategoriaId",
                table: "Artigos");

            migrationBuilder.DropIndex(
                name: "IX_Artigos_TemaId",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "DataImpulsionamento",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "DataInativacao",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "DataPublicacao",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "DataRemocao",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Artigos");

            migrationBuilder.DropColumn(
                name: "TemaId",
                table: "Artigos");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Artigos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Artigos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Artigos",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
