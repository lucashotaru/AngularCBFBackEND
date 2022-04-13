using System.IdentityModel.Tokens.Jwt;
using AngularCBFBackEND.Identity.Factory;
using AngularCBFBackEND.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.Identity.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController: ControllerBase
    {
        public static UserManager<IdentityUser> _gerenciarUsuario { get; private set;}
        public static RoleManager<IdentityRole> _gerenciarPapeis { get; private set;}
        public static IConfiguration _configuration { get; private set;}

        public IdentityController(UserManager<IdentityUser> gerenciarUsuario,
                                  RoleManager<IdentityRole> gerenciarPapeis,
                                  IConfiguration configuration)
        {
            _gerenciarUsuario = gerenciarUsuario;
            _gerenciarPapeis = gerenciarPapeis;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] IdentityLoginModel model)
        {
            var resultado = await IdentityFactory.FazerLogin(model);

            if(resultado != null)
            {
                return Ok(
                    new{
                        resultado = new JwtSecurityTokenHandler().WriteToken(resultado),
                        expiration = resultado.ValidTo
                    }
                );    
            };

            return Unauthorized();
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Register([FromBody] IdentityRegistroModel model)
        {
            var usuarioExistente = await IdentityFactory.VerificaSeUsuarioJaExiste(model);
            if (usuarioExistente != false)
                return StatusCode(StatusCodes.Status500InternalServerError, new IdentityRetorno { Status = "Erro", Message = "Usuario já existe!" });
            
            try
            {
                var resultado = await IdentityFactory.CriarNovoUsuario(model);

                if(!resultado)
                    return StatusCode(StatusCodes.Status500InternalServerError, new IdentityRetorno { Status = "Erro", Message = "Falha ao tentar criar o usuario, por favor verifique se as informações estão corretas." });
            }
            catch (System.Exception)
            {
                throw;
            }

            return Ok(new IdentityRetorno { Status = "Sucesso", Message = "Usuario criado com sucesso!" });
        }  
    }
}