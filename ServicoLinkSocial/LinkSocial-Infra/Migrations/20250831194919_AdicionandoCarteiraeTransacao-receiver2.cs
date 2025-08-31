using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCarteiraeTransacaoreceiver2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_ReceiverId",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "ValorParcela",
                table: "Doacoes",
                newName: "Valor");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Transacoes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "Criado_em",
                table: "Transacoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Transacoes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modificado_em",
                table: "Transacoes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_ReceiverId",
                table: "Transacoes",
                column: "ReceiverId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Usuarios_ReceiverId",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Criado_em",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Modificado_em",
                table: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Doacoes",
                newName: "ValorParcela");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Transacoes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Usuarios_ReceiverId",
                table: "Transacoes",
                column: "ReceiverId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
