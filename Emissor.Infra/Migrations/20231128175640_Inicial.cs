using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emissor.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "clientes");

            migrationBuilder.EnsureSchema(
                name: "usuarios");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "clientes",
                schema: "clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    endereco = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    endereco_numero = table.Column<int>(type: "integer", nullable: false),
                    bairro = table.Column<string>(type: "text", nullable: false),
                    municipio = table.Column<string>(type: "text", nullable: false),
                    is_pj = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                schema: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    nome_usuario = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    senha = table.Column<string>(type: "character varying(72)", maxLength: 72, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_documento",
                schema: "clientes",
                table: "clientes",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_nome_usuario",
                schema: "usuarios",
                table: "usuarios",
                column: "nome_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes",
                schema: "clientes");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "usuarios");
        }
    }
}
