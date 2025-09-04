using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkSocial_Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoComentarioUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "Usuarios",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "Usuarios");
        }
    }
}
