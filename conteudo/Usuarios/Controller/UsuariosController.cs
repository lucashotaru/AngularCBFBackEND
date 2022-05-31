using AngularCBFBackEND.conteudo.Tabelas.Models;
using AngularCBFBackEND.conteudo.Usuarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AngularCBFBackEND.conteudo.Usuarios.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public UsuariosController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }
        
        [HttpGet]
        [Route("getUsuarios")]
        public async Task<IActionResult> getUsuarios()
        {
            List<UsuariosModel> ListaUsuarios = new List<UsuariosModel>();

            try
            {

                return Ok();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }    
    }
}