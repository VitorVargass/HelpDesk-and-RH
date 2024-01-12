using Microsoft.EntityFrameworkCore;


namespace HD_SUPPORT.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Administrador> Administradores { get; set; }

        public DbSet<Equipamento> Equipamentos { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
