using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularCBFBackEND.Identity.Factory;
using AngularCBFBackEND.Identity.Models;
using AngularCBFBackEND.Identity.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.Identity.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController: ControllerBase
    {
        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Register([FromBody] IdentityRegistroModel model)
        {
            var usuarioExistente = await IdentityFactory.VerificaSeUsuarioExiste(model);
            if (usuarioExistente == true)
                return StatusCode(StatusCodes.Status500InternalServerError, new IdentityRetorno { Status = "Erro", Message = "Usuario já existe!" });
            
            try
            {
                
            }
            catch (System.Exception)
            {
                throw;
            }

            return Unauthorized();
        }        
    }
}