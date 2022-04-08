using System.ComponentModel.DataAnnotations;

namespace AngularCBFBackEND.Identity.Models
{
    public class IdentityRegistroModel
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string? Password { get; set; }
        
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public int Funcao { get; set; }
    }
}