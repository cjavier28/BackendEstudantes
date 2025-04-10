using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SGEU.WebApi.DataAccess;
using SGEU.WebApi.DTOS;

namespace SGEU.WebApi.Services
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

        public async Task<IEnumerable<ProgramaDTO>> GetProgramas()
        {
            return await _db.Programas
                .ProjectTo<ProgramaDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        
    }
}
