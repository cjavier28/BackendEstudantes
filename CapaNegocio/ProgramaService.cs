using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;
using ServicioGestionEstudiantes.Datos;
using ServicioGestionEstudiantes.Negocio.DTOS;


namespace ServicioGestionEstudiantes.Negocio
{
    public class ProgramaService
    {
        private readonly DBContext _db;
        private readonly IMapper _mapper;

        public ProgramaService(DBContext dBContext, IMapper mapper)
        {
            _db = dBContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProgramaDto>> GetProgramas()
        {
            return await _db.Programas
                .ProjectTo<ProgramaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        
    }
}
