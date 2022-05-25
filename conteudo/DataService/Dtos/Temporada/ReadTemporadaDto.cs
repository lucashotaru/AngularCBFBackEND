using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCBFBackEND.Data.Dtos.Temporada
{
    public class ReadTemporadaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Ano é obrigatório")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O campo Ano deve possuir 4 caracteres")]
        public string Ano { get; set; }

        public object Times { get; set; }
    }
}
