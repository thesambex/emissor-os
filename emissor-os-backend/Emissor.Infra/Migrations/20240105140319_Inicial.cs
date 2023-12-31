﻿using System;
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
                name: "estoque");

            migrationBuilder.EnsureSchema(
                name: "ordens_servico");

            migrationBuilder.EnsureSchema(
                name: "usuarios");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:tipo_unidades", "unidade,metro,kilo,litro")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateSequence(
                name: "numero_os_seq");

            migrationBuilder.CreateTable(
                name: "clientes",
                schema: "clientes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    endereco = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    endereco_numero = table.Column<int>(type: "INTEGER", nullable: false),
                    bairro = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    municipio = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    is_pj = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mercadorias",
                schema: "estoque",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    descricao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    referencia = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    codigo_barra = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    preco = table.Column<double>(type: "numeric(8,2)", nullable: false),
                    unidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mercadorias", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "ordens_servico",
                schema: "ordens_servico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    numero = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('numero_os_seq')"),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    atendente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao = table.Column<string>(type: "text", nullable: false),
                    observacoes = table.Column<string>(type: "text", nullable: true),
                    valor_hora = table.Column<double>(type: "numeric(8,2)", nullable: false),
                    valor_final = table.Column<double>(type: "numeric(10,2)", nullable: false),
                    dt_inicio = table.Column<DateTimeOffset>(type: "TIMESTAMPTZ", nullable: false),
                    dt_fim = table.Column<DateTimeOffset>(type: "TIMESTAMPTZ", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordens_servico", x => x.id);
                    table.ForeignKey(
                        name: "FK_ordens_servico_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalSchema: "clientes",
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordens_servico_usuarios_atendente_id",
                        column: x => x.atendente_id,
                        principalSchema: "usuarios",
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordens_servico_mercadoias",
                schema: "ordens_servico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    mercadoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ordem_servico_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantidade = table.Column<double>(type: "numeric(7,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordens_servico_mercadoias", x => x.id);
                    table.ForeignKey(
                        name: "FK_ordens_servico_mercadoias_mercadorias_mercadoria_id",
                        column: x => x.mercadoria_id,
                        principalSchema: "estoque",
                        principalTable: "mercadorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordens_servico_mercadoias_ordens_servico_ordem_servico_id",
                        column: x => x.ordem_servico_id,
                        principalSchema: "ordens_servico",
                        principalTable: "ordens_servico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_documento",
                schema: "clientes",
                table: "clientes",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mercadorias_referencia",
                schema: "estoque",
                table: "mercadorias",
                column: "referencia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_atendente_id",
                schema: "ordens_servico",
                table: "ordens_servico",
                column: "atendente_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_cliente_id",
                schema: "ordens_servico",
                table: "ordens_servico",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_mercadoias_mercadoria_id",
                schema: "ordens_servico",
                table: "ordens_servico_mercadoias",
                column: "mercadoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_ordens_servico_mercadoias_ordem_servico_id",
                schema: "ordens_servico",
                table: "ordens_servico_mercadoias",
                column: "ordem_servico_id");

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
                name: "ordens_servico_mercadoias",
                schema: "ordens_servico");

            migrationBuilder.DropTable(
                name: "mercadorias",
                schema: "estoque");

            migrationBuilder.DropTable(
                name: "ordens_servico",
                schema: "ordens_servico");

            migrationBuilder.DropTable(
                name: "clientes",
                schema: "clientes");

            migrationBuilder.DropTable(
                name: "usuarios",
                schema: "usuarios");

            migrationBuilder.DropSequence(
                name: "numero_os_seq");
        }
    }
}
