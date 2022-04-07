using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AngularCBFBackEND.Identity.Controller;
using AngularCBFBackEND.Identity.Factory;
using Microsoft.IdentityModel.Tokens;

namespace AngularCBFBackEND.Identity.Repositories
{
    public class IdentityRepository
    {
        public static JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IdentityFactory._configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: IdentityFactory._configuration["JWT:ValidIssuer"],
                audience: IdentityFactory._configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}