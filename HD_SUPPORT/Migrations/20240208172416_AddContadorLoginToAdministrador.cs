using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HD_SUPPORT.Migrations
{
    /// <inheritdoc />
    public partial class AddContadorLoginToAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headset",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "SistemaOperacional",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "TelegramUsuario",
                table: "Equipamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Headset",
                table: "Equipamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SistemaOperacional",
                table: "Equipamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelegramUsuario",
                table: "Equipamentos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
