using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Models
{
    public class FileModel
    {
        public IFormFile arquivo { get; set; }
    }
}