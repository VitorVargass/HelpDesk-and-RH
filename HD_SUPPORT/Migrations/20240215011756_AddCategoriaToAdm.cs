using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HD_SUPPORT.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaToAdm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Administradores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Administradores");
        }
    }
}
