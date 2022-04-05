using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCBFBackEND.Identity.Models
{
    public class IdentityRegistroModel
    {
        [Required(ErrorMessage = "Login é obrigatorio")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatoria")]
        public string? Password { get; set; }
    }
}