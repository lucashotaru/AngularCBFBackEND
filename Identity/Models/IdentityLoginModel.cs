using System.ComponentModel.DataAnnotations;

namespace AngularCBFBackEND.Identity.Models
{
    public class IdentityLoginModel
    {
        [Required(ErrorMessage = "Usuario incorreto")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Senha incorreta")]
        public string? Password { get; set; }
    }
}