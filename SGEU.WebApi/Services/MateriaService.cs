﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGEU.WebApi.DataAccess;
using SGEU.WebApi.DTOS;

namespace SGEU.WebApi.Services
{
    public class MateriaService
    {
        private readonly DBContext _db;
        private readonly IMapper _mapper;

        public MateriaService(DBContext dBContext, IMapper mapper)
        {
            _db = dBContext;
            _mapper = mapper;
        }       

        public async Task<IEnumerable<ProgramaMateriaDTO>> GetMateriasPrograma(int idPrograma)
        {
            return await _db.Programas
                .Where(p => p.IdPrograma == idPrograma)
                .SelectMany(p => p.IdMateria)
                .ProjectTo<ProgramaMateriaDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<MateriasEstudianteDTO>> GetMateriaByEstudiante(string idEstudiante)
        {
            var materias = await _db.Materia
                .Where(m => m.IdEstudiantes.Any(e => e.IdEstudiante == idEstudiante)) 
                .Include(m => m.IdProfesors)
                .Include(m => m.IdProgramas)
                .ToListAsync();

            return _mapper.Map<List<MateriasEstudianteDTO>>(materias);
        }     

    }
}
