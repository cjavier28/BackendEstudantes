using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicioGestionEstudiantes.Entidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGestionEstudiantes.Seguridad
{
    public class JwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Estudiante estudiante)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, estudiante.IdEstudiante.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, estudiante.Email),
            new Claim("nombre", estudiante.NombresEstudiante),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],  
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
