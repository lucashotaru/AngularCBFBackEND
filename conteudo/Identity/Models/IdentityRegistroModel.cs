using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AngularCBFBackEND.Identity.Models
{
    public class IdentityRegistroModel
    {
        [Required(ErrorMessage = "Login é obrigatório")]
        public string Login { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
        
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public int Funcao { get; set; }
        public string ProfilePicture { get; set; }
    }
}