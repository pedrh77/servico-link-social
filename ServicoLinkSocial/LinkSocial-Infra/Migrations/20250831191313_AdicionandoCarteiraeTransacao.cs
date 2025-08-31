using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCarteiraeTransacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficos_Usuarios_UsuarioId",
                table: "Beneficos");

            migrationBuilder.DropForeignKey(
                name: "FK_Doacoes_Beneficos_BeneficioId",
                table: "Doacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficos",
                table: "Beneficos");

            migrationBuilder.RenameTable(
                name: "Beneficos",
                newName: "Beneficios");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Doacoes",
                newName: "ValorParcela");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficos_UsuarioId",
                table: "Beneficios",
                newName: "IX_Beneficios_UsuarioId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Doacoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficios",
                table: "Beneficios",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Carteiras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Saldo = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modificado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteiras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarteiraId = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    OngId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Carteiras_CarteiraId",
                        column: x => x.CarteiraId,
                        principalTable: "Carteiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacoes_Usuarios_OngId",
                        column: x => x.OngId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CarteiraId",
                table: "Transacoes",
                column: "CarteiraId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_OngId",
                table: "Transacoes",
                column: "OngId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficios_Usuarios_UsuarioId",
                table: "Beneficios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Beneficios_BeneficioId",
                table: "Doacoes",
                column: "BeneficioId",
                principalTable: "Beneficios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficios_Usuarios_UsuarioId",
                table: "Beneficios");

            migrationBuilder.DropForeignKey(
                name: "FK_Doacoes_Beneficios_BeneficioId",
                table: "Doacoes");

            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Carteiras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficios",
                table: "Beneficios");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Doacoes");

            migrationBuilder.RenameTable(
                name: "Beneficios",
                newName: "Beneficos");

            migrationBuilder.RenameColumn(
                name: "ValorParcela",
                table: "Doacoes",
                newName: "Valor");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficios_UsuarioId",
                table: "Beneficos",
                newName: "IX_Beneficos_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficos",
                table: "Beneficos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficos_Usuarios_UsuarioId",
                table: "Beneficos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Beneficos_BeneficioId",
                table: "Doacoes",
                column: "BeneficioId",
                principalTable: "Beneficos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
