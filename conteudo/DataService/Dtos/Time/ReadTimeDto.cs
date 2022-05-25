using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCBFBackEND.Data.Dtos
{
    public class ReadTimeDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Ataque é obrigatório")]
        [Range(1, 100, ErrorMessage = "O valor do campo Ataque deve estar entre 1 e 100")]
        public int Ataque { get; set; }

        [Required(ErrorMessage = "O campo Meio Campo é obrigatório")]
        [Range(1, 100, ErrorMessage = "O valor do campo Meio Campo deve estar entre 1 e 100")]
        public int MeioCampo { get; set; }

        [Required(ErrorMessage = "O campo Defesa é obrigatório")]
        [Range(1, 100, ErrorMessage = "O valor do campo Defesa deve estar entre 1 e 100")]
        public int Defesa { get; set; }

        public Models.Temporada Temporada { get; set; }
    }
}
