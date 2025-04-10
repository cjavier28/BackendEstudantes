
using Microsoft.EntityFrameworkCore;
using ServicioGestionEstudiantes.Entidades;
using Microsoft.Extensions.Configuration;
using ServicioGestionEstudiantes.Negocio.DTOS;
using BCrypt.Net;
using ServicioGestionEstudiantes.Datos;
using ServicioGestionEstudiantes.Seguridad;

namespace ServicioGestionEstudiantes.Negocio
{
    public class AuthService
    {

        private readonly DBContext _db;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;
        public AuthService(DBContext dBContext, IConfiguration configuration, JwtService jwtService)
        {
            _db = dBContext;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task<Token> Login(LoginDTO loginDto)
        {

            Token token = new Token();          


            var usuario = await _db.Estudiantes
                .FirstOrDefaultAsync(e => e.Email == loginDto.Email);

            if (usuario == null)
                throw new Exception("Usuario no encontrado.");


            if (!BCrypt.Net.BCrypt.Verify(loginDto.Contrasena, usuario.Contrasena))
                throw new Exception("Contraseña incorrecta");


            token.TokenBearer = _jwtService.GenerateToken(usuario);
            return token;
        }

    }
}
