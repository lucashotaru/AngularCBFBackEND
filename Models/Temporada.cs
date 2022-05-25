using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AngularCBFBackEND.Models
{
    public class Temporada
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Ano é obrigatório")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "O campo Ano deve possuir 4 caracteres")]
        public string Ano { get; set; }

        [JsonIgnore]
        public virtual List<Time> Times { get; set; }

    }
}
