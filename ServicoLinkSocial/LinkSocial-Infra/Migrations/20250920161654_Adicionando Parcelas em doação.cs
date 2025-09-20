using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoParcelasemdoação : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoacaoPrincipalId",
                table: "Doacoes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_DoacaoPrincipalId",
                table: "Doacoes",
                column: "DoacaoPrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doacoes_Doacoes_DoacaoPrincipalId",
                table: "Doacoes",
                column: "DoacaoPrincipalId",
                principalTable: "Doacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doacoes_Doacoes_DoacaoPrincipalId",
                table: "Doacoes");

            migrationBuilder.DropIndex(
                name: "IX_Doacoes_DoacaoPrincipalId",
                table: "Doacoes");

            migrationBuilder.DropColumn(
                name: "DoacaoPrincipalId",
                table: "Doacoes");
        }
    }
}
