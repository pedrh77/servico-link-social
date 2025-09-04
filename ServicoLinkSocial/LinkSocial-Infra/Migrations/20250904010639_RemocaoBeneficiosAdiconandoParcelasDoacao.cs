using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoBeneficiosAdiconandoParcelasDoacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doacoes_Beneficios_BeneficioId",
                table: "Doacoes");

            migrationBuilder.DropTable(
                name: "Beneficios");

            migrationBuilder.DropIndex(
                name: "IX_Doacoes_BeneficioId",
                table: "Doacoes");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Doacoes");

            migrationBuilder.RenameColumn(
                name: "BeneficioId",
                table: "Doacoes",
                newName: "TotalParcelas");

            migrationBuilder.AddColumn<int>(
                name: "NumeroParcela",
                table: "Doacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroParcela",
                table: "Doacoes");

            migrationBuilder.RenameColumn(
                name: "TotalParcelas",
                table: "Doacoes",
                newName: "BeneficioId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Doacoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Beneficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Modificado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_BeneficioId",
                table: "Doacoes",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficios_UsuarioId",
                table: "Beneficios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Beneficios_BeneficioId",
                table: "Doacoes",
                column: "BeneficioId",
                principalTable: "Beneficios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
