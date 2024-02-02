namespace HD_SUPPORT.Models
{
    public class Administrador
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }
        
        // public required string CodigoConfirmacao { get; set;}

    }
}