using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AngularCBFBackEND.Identity.Controller;
using AngularCBFBackEND.Identity.Models;
using AngularCBFBackEND.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.Identity.Factory
{
    public class IdentityFactory
    {
        public static async Task<bool> VerificaSeUsuarioJaExiste([FromBody] IdentityRegistroModel model)
        {
            var usuarioExistente = await IdentityController._gerenciarUsuario.FindByEmailAsync(model.Username);
            if (usuarioExistente != null)
                return true;

            return false;
        }

        public static async Task<bool> CriarNovoUsuario([FromBody] IdentityRegistroModel model)
        {
            IdentityUser usuario = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            
            var resultado = await IdentityController._gerenciarUsuario.CreateAsync(usuario, model.Password);

            if(model.Funcao == 1){
                if (!await IdentityController._gerenciarPapeis.RoleExistsAsync(IdentityPapeisModel.Admin))
                await IdentityController._gerenciarPapeis.CreateAsync(new IdentityRole(IdentityPapeisModel.Admin));
            if (!await IdentityController._gerenciarPapeis.RoleExistsAsync(IdentityPapeisModel.User))
                await IdentityController._gerenciarPapeis.CreateAsync(new IdentityRole(IdentityPapeisModel.User));

            if (await IdentityController._gerenciarPapeis.RoleExistsAsync(IdentityPapeisModel.Admin))
            {
                await IdentityController._gerenciarUsuario.AddToRoleAsync(usuario, IdentityPapeisModel.Admin);
            }
            if (await IdentityController._gerenciarPapeis.RoleExistsAsync(IdentityPapeisModel.Admin))
            {
                await IdentityController._gerenciarUsuario.AddToRoleAsync(usuario, IdentityPapeisModel.User);
            }
            }

            return resultado.Succeeded;
        }

        public static async Task<JwtSecurityToken> FazerLogin([FromBody] IdentityLoginModel model)
        {
            var usuario = await IdentityController._gerenciarUsuario.FindByNameAsync(model.Username);

            if(usuario != null && await IdentityController._gerenciarUsuario.CheckPasswordAsync(usuario, model.Password))
            {
                var papeisUsuarios = await IdentityController._gerenciarUsuario.GetRolesAsync(usuario);

                var autenticacaoClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var papeisUsuario in papeisUsuarios)
                {
                    autenticacaoClaim.Add(new Claim(ClaimTypes.Role, papeisUsuario));
                }

                var token = IdentityRepository.GetToken(autenticacaoClaim);

                return token;
            }
            return null;
        }
    }
}