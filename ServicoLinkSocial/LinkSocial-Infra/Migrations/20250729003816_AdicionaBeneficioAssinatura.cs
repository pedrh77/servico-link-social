using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaBeneficioAssinatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beneficos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Criado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modificado_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OngId = table.Column<int>(type: "integer", nullable: false),
                    DoadorId = table.Column<int>(type: "integer", nullable: false),
                    TipoDoacao = table.Column<int>(type: "integer", nullable: false),
                    Inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Termina_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Pago = table.Column<bool>(type: "boolean", nullable: false),
                    AssinaturaId = table.Column<int>(type: "integer", nullable: false),
                    BeneficioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Beneficos_BeneficioId",
                        column: x => x.BeneficioId,
                        principalTable: "Beneficos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Usuarios_DoadorId",
                        column: x => x.DoadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Usuarios_OngId",
                        column: x => x.OngId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_BeneficioId",
                table: "Assinaturas",
                column: "BeneficioId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_DoadorId",
                table: "Assinaturas",
                column: "DoadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_OngId",
                table: "Assinaturas",
                column: "OngId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficos_UsuarioId",
                table: "Beneficos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinaturas");

            migrationBuilder.DropTable(
                name: "Beneficos");
        }
    }
}
