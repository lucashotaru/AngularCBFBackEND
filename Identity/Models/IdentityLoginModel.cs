using System.ComponentModel.DataAnnotations;

namespace AngularCBFBackEND.Identity.Models
{
    public class IdentityLoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}