namespace HD_SUPPORT.Models
{
    public class Equipamento
    {
        public int Id { get; set; }

        public required string NomeUsuario { get; set; }

        public required string EmailUsuario { get; set; }

        public required string TelefoneUsuario { get; set; }

        public required string StatusUsuario { get; set; }

        public required string CategoriaUsuario { get; set; }

        public int IdPatrimonio { get; set; }

        public required string ModeloMarca { get; set; }

        public required string Processador { get; set; }

        public required string InicioEmprestimo { get; set; }

        public required string TerminoEmprestimo { get; set; }
    }
}