using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularCBFBackEND.Identity.Controller;
using AngularCBFBackEND.Identity.Factory;
using AngularCBFBackEND.Identity.Models;
using AngularCBFBackEND.Identity.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.Identity.Factory
{
    public class IdentityFactory
    {
        public static async Task<bool> VerificaSeUsuarioJaExiste([FromBody] IdentityRegistroModel model)
        {
            var usuarioExistente = await IdentityController._userManager.FindByEmailAsync(model.Username);
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
            var resultado = await IdentityController._userManager.CreateAsync(usuario, model.Password);

            if(model.Funcao == 1){
                if (!await IdentityController._roleManager.RoleExistsAsync(IdentityFuncaoModel.Admin))
                await IdentityController._roleManager.CreateAsync(new IdentityRole(IdentityFuncaoModel.Admin));
            if (!await IdentityController._roleManager.RoleExistsAsync(IdentityFuncaoModel.User))
                await IdentityController._roleManager.CreateAsync(new IdentityRole(IdentityFuncaoModel.User));

            if (await IdentityController._roleManager.RoleExistsAsync(IdentityFuncaoModel.Admin))
            {
                await IdentityController._userManager.AddToRoleAsync(usuario, IdentityFuncaoModel.Admin);
            }
            if (await IdentityController._roleManager.RoleExistsAsync(IdentityFuncaoModel.Admin))
            {
                await IdentityController._userManager.AddToRoleAsync(usuario, IdentityFuncaoModel.User);
            }
            }

            return resultado.Succeeded;
        }

        public static async Task<JwtSecurityToken> FazerLogin([FromBody] IdentityLoginModel model)
        {
            var usuario = await IdentityController._userManager.FindByNameAsync(model.Username);

            if(usuario != null && await IdentityController._userManager.CheckPasswordAsync(usuario, model.Password))
            {
                var papeisUsuarios = await IdentityController._userManager.GetRolesAsync(usuario);

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