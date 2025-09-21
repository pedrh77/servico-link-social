using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoSaldosDinamicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Carteiras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Carteiras",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
