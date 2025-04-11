
using Microsoft.EntityFrameworkCore;
using ServicioGestionEstudiantes.Entidades;
using Microsoft.Extensions.Configuration;
using ServicioGestionEstudiantes.Negocio.DTOS;
using BCrypt.Net;
using ServicioGestionEstudiantes.Datos;
using ServicioGestionEstudiantes.Seguridad;
using Newtonsoft;
using Newtonsoft.Json;
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

        public async Task<Token> Login(LoginDto loginDto)
        {
            Token token = new Token();
            try
            {
               

                Estudiante? usuario = await _db.Estudiantes
                    .FirstOrDefaultAsync(e => e.Email == loginDto.Email);

                if (usuario == null)
                    throw new InvalidOperationException("Usuario no encontrado.");


                if (!BCrypt.Net.BCrypt.Verify(loginDto.Contrasena, usuario.Contrasena))
                    throw new InvalidOperationException("Contraseña incorrecta");


                token.TokenBearer = _jwtService.GenerateToken(usuario);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonConvert.SerializeObject(ex));
            }
            return token;
        }

    }
}
