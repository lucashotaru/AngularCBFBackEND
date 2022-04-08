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
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IdentityController._configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: IdentityController._configuration["JWT:ValidIssuer"],
                audience: IdentityController._configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}