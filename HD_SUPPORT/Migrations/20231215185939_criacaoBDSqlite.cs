using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HD_SUPPORT.Migrations
{
    /// <inheritdoc />
    public partial class criacaoBDSqlite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    EmailUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    TelegramUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    TelefoneUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    StatusUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    CategoriaUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    IdPatrimonio = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloMarca = table.Column<string>(type: "TEXT", nullable: false),
                    Processador = table.Column<string>(type: "TEXT", nullable: false),
                    SistemaOperacional = table.Column<string>(type: "TEXT", nullable: false),
                    Headset = table.Column<string>(type: "TEXT", nullable: false),
                    InicioEmprestimo = table.Column<string>(type: "TEXT", nullable: false),
                    TerminoEmprestimo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Equipamentos");
        }
    }
}
