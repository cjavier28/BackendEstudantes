using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ServicioGestionEstudiantes.Datos;
using ServicioGestionEstudiantes.Negocio.DTOS;


namespace ServicioGestionEstudiantes.Negocio
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

        public async Task<IEnumerable<ProgramaMateriaDto>> GetMateriasPrograma(int idPrograma)
        {
            return await _db.Programas
                .Where(p => p.IdPrograma == idPrograma)
                .SelectMany(p => p.IdMateria)
                .ProjectTo<ProgramaMateriaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<MateriasEstudianteDto>> GetMateriaByEstudiante(string idEstudiante)
        {
            List<Entidades.Materia> materias = await _db.Materia
                .Where(m => m.IdEstudiantes.Any(e => e.IdEstudiante == idEstudiante)) 
                .Include(m => m.IdProfesors)
                .Include(m => m.IdProgramas)
                .ToListAsync();

            return _mapper.Map<List<MateriasEstudianteDto>>(materias);
        }     

    }
}
