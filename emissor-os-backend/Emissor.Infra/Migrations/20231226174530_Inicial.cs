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
                name: "ordens_servico");

            migrationBuilder.EnsureSchema(
                name: "estoque");

            migrationBuilder.EnsureSchema(
                name: "usuarios");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:tipo_unidades", "unidade,metro,kilo,litro")
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
                name: "ordems_servico",
                schema: "ordens_servico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    numero = table.Column<long>(type: "SERIAL NOT NULL", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atendente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    observacoes = table.Column<string>(type: "text", nullable: true),
                    dt_inicio = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordems_servico", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                schema: "estoque",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    descricao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    referencia = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    codigo_barra = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    unidade = table.Column<int>(type: "tipo_unidades", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.id);
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
                name: "IX_produtos_referencia",
                schema: "estoque",
                table: "produtos",
                column: "referencia",
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
                name: "ordems_servico",
                schema: "ordens_servico");

            migrationBuilder.DropTable(
                name: "produtos",
                schema: "estoque");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "usuarios");
        }
    }
}
