using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public static IConfiguration _configuration;


        public IdentityFactory(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public static async Task<bool> VerificaSeUsuarioExiste([FromBody] IdentityRegistroModel model)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(model.Username);
            if(usuarioExistente == null){
                return true;
            }

            return false; 
        }


    }
}