﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGEU.WebApi.DataAccess;
using SGEU.WebApi.DTOS;

namespace SGEU.WebApi.Services
{
    public class EstudianteService
    {
        private readonly DBContext _db;
        private readonly IMapper _mapper;

        public EstudianteService(DBContext dBContext, IMapper mapper)
        {
            _db = dBContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantes()
        {
            return await _db.Estudiantes.ToListAsync();
        }

        public async Task<IEnumerable<EstudianteProgramaDTO>> GetEstudiantesByPrograma(int idPrograma)
        {
            var estudiantes = await _db.Estudiantes
                .Where(e => e.IdPrograma == idPrograma)
                .Include(e => e.IdProgramaNavigation)
                .ToListAsync();
            return _mapper.Map<List<EstudianteProgramaDTO>>(estudiantes);
        }

        public async Task<IEnumerable<EstudianteMateriaDTO>> GetEstudianteByMateria(int idMateria)
        {
            var estudiantes = await _db.Estudiantes
                .Where(e => e.IdPrograma == idMateria)
                .Include(e => e.IdProgramaNavigation)
                .ToListAsync();
            return _mapper.Map<List<EstudianteMateriaDTO>>(estudiantes);
        }

        public async Task<string> CreateEstudiante(EstudianteDTO estudiante)
        {
            var estudiant = await _db.Estudiantes
                .Where(e => e.IdEstudiante == estudiante.IdEstudiante)
                .FirstOrDefaultAsync();

            if (estudiant != null) throw new Exception($"El estudiante con número de cédula {estudiante.IdEstudiante} ya está registrado.");

            if (string.IsNullOrEmpty(estudiante.IdEstudiante)) throw new Exception("Por favor ingrese su número de documento.");

            if (string.IsNullOrEmpty(estudiante.NombresEstudiante)) throw new Exception("Por favor ingrese  sus nombres.");

            if (string.IsNullOrEmpty(estudiante.ApellidosEstudiante)) throw new Exception("Por favor ingrese  sus apellido.");

            if (string.IsNullOrEmpty(estudiante.Contrasena)) throw new Exception("Por favor ingrese una contraseña.");
            
            if (string.IsNullOrEmpty(estudiante.Email)) throw new Exception("Por favor ingrese su Email.");


            // Si el IdPrograma no es nulo, verificar si el programa existe
            if (estudiante.IdPrograma != null)
            {
                var programa = await _db.Programas
                    .Where(p => p.IdPrograma == estudiante.IdPrograma)
                    .FirstOrDefaultAsync();

                if (programa == null)
                    throw new Exception($"El programa con ID {estudiante.IdPrograma} no existe.");
            }

            // Generar un salt único para la contraseña
            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Encriptar la contraseña usando el salt generado
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(estudiante.Contrasena, salt);

            var newEstudiante = _mapper.Map<Estudiante>(estudiante);

            newEstudiante.Contrasena = hashedPassword;
            newEstudiante.Salt = salt;

            await _db.AddAsync(newEstudiante);
            await _db.SaveChangesAsync();

            return "Estudiante creado correctamete!";
        }

        public async Task<string> UpdateEstudiante(string idEstudiante, int idPrograma)
        {
            var estudiante = await _db.Estudiantes
                                      .Where(e => e.IdEstudiante == idEstudiante)
                                      .FirstOrDefaultAsync();

            if (estudiante == null)
                throw new Exception($"El estudiante con número de cédula {idEstudiante} no existe.");

            var materiasRegistradas = await _db.Estudiantes
                                                .Where(e => e.IdEstudiante == idEstudiante)
                                                .SelectMany(e => e.IdMateria)
                                                .ToListAsync();

            if (materiasRegistradas.Any())
                throw new Exception("No se puede cambiar de programa. El estudiante ya tiene materias registradas.");

            if (estudiante.IdPrograma == idPrograma)
                throw new Exception("El estudiante ya está inscrito en este programa.");

            estudiante.IdPrograma = idPrograma;

            _db.Estudiantes.Update(estudiante);
            await _db.SaveChangesAsync();

            return "Programa actualizado exitosamente";
        }

        public async Task<string> RegistrarMateriasEstudiante(string idEstudiante, List<int> idMaterias)
        {
            var estudiante = await _db.Estudiantes
                .Include(e => e.IdMateria)
                .Include(e => e.IdProgramaNavigation)  
                .FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);

            if (estudiante == null)
                throw new Exception("Estudiante no encontrado.");

            if (estudiante.IdMateria.Count >= 3)
                throw new Exception("El estudiante ya tiene tres materias registradas. No puede agregar más.");

            bool esValido = await ValidarMaterias(idMaterias);

            if (!esValido)
                throw new Exception("Las materias seleccionadas no son válidas. Debes seleccionar un máximo de tres materias, y no puedes seleccionar más de una materia del mismo profesor.");

            var programaEstudiante = estudiante.IdProgramaNavigation;  // El programa al que el estudiante está inscrito

            var programasMateriasSeleccionadas = await _db.Materia
                .Where(m => idMaterias.Contains(m.IdMateria))    // Filtrar las materias seleccionadas
                .SelectMany(m => m.IdProgramas)   // Obtener los programas asociados a las materias
                .Distinct()   // Eliminar duplicados
                .ToListAsync();

            if (programasMateriasSeleccionadas.Count > 0 && programasMateriasSeleccionadas.First() != programaEstudiante)
                throw new Exception("Las materias seleccionadas no pertenecen al mismo programa en el que el estudiante está inscrito.");

            // Registrar las nuevas materias
            foreach (var materiaId in idMaterias)
            {
                var materia = await _db.Materia.FindAsync(materiaId);
                if (materia != null)
                {
                    estudiante.IdMateria.Add(materia);
                }
            }

            await _db.SaveChangesAsync();  

            return "Materias registradas correctamente.";  
        }

        public async Task<bool> ValidarMaterias(List<int> idMaterias)
        {
            if (idMaterias.Count > 3)
                return false; 

            var materiasSeleccionadas = await _db.Materia
                .Where(m => idMaterias.Contains(m.IdMateria))
                .Include(m => m.IdProfesors) 
                .ToListAsync();

            var profesoresSeleccionados = materiasSeleccionadas
                .SelectMany(m => m.IdProfesors)
                .GroupBy(p => p.IdProfesor)
                .Where(g => g.Count() > 1)
                .ToList();

            if (profesoresSeleccionados.Any())
                return false; 

            return true; 
        }

        public async Task<string> DeleteMateriaEstudiante(string idEstudiante, int idMateria)
        {
           var estudiante = await _db.Estudiantes
                .Include(e => e.IdMateria) 
                .FirstOrDefaultAsync(e => e.IdEstudiante == idEstudiante);

            if (estudiante == null)
                throw new Exception("Estudiante no encontrado.");

            var materia = estudiante.IdMateria.FirstOrDefault(m => m.IdMateria == idMateria);

            if (materia == null)
                throw new Exception($"El estudiante no está inscrito en la materia con ID {idMateria}.");

            estudiante.IdMateria.Remove(materia);

            await _db.SaveChangesAsync();

            return "Materia eliminada correctamente del estudiante.";
        }
    }
}
