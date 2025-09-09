using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoPedidodois : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "Pedidos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaId",
                table: "Pedidos",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_EmpresaId",
                table: "Pedidos",
                column: "EmpresaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_EmpresaId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_EmpresaId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Pedidos");
        }
    }
}
