using System.ComponentModel.DataAnnotations;

namespace HD_SUPPORT.Models
{
    public class AtualizarSenhaViewModel
    {
        public string Token { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A senha deve ter pelo menos 6 caracteres!", MinimumLength = 6)]
        public string NovaSenha { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas precisam ser iguais!.")] 
        public string ConfirmarSenha { get; set; }
    }
}
