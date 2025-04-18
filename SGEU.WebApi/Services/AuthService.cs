﻿using Microsoft.EntityFrameworkCore;
using SGEU.WebApi.DataAccess;
using SGEU.WebApi.DTOS;

namespace SGEU.WebApi.Services
{
    public class AuthService
    {

        private readonly DBContext _db;
        private readonly IConfiguration _configuration;

        public AuthService(DBContext dBContext, IConfiguration configuration)
        {
            _db = dBContext;
            _configuration = configuration;
        }

        public async Task<Estudiante> Login(LoginDTO loginDto)
        {
            var usuario = await _db.Estudiantes
                .FirstOrDefaultAsync(e => e.Email == loginDto.Email);

            if (usuario == null)
                throw new Exception("Usuario no encontrado.");


            if (!BCrypt.Net.BCrypt.Verify(loginDto.Contrasena, usuario.Contrasena))
                throw new Exception("Contraseña incorrecta");

            return usuario;
        }

    }
}
