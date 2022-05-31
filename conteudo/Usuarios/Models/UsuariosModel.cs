using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCBFBackEND.conteudo.Usuarios.Models
{
    public class UsuariosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String fotoUrl { get; set; }
        public String nomeUsuario { get; set; }
    }
}