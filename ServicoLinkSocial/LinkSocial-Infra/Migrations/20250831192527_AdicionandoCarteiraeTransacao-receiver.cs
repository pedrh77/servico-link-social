using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCarteiraeTransacaoreceiver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_OngId",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "OngId",
                table: "Transacoes",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_OngId",
                table: "Transacoes",
                newName: "IX_Transacoes_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_ReceiverId",
                table: "Transacoes",
                column: "ReceiverId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_ReceiverId",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Transacoes",
                newName: "OngId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_ReceiverId",
                table: "Transacoes",
                newName: "IX_Transacoes_OngId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_OngId",
                table: "Transacoes",
                column: "OngId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
