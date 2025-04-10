using AutoMapper;
using ServicioGestionEstudiantes.Entidades;
using SGEU.WebApi.DTOS;

namespace SGEU.WebApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Programa
            CreateMap<Programa, ProgramaDTO>().ReverseMap();      

            //Materia
            CreateMap<Materia, MateriaDTO>().ReverseMap();

            CreateMap<Materia, ProgramaMateriaDTO>()
                .ForMember(dest => dest.IdPrograma, opt => opt.MapFrom(src => src.IdProgramas.FirstOrDefault().IdPrograma))
                .ForMember(dest => dest.IdProfesor,opt => opt.MapFrom(src => src.IdProfesors.FirstOrDefault().IdProfesor))
                .ForMember(dest => dest.NombreProfesor,opt => opt.MapFrom(src => $"{src.IdProfesors.FirstOrDefault().NombresProfesor} {src.IdProfesors.FirstOrDefault().ApellidosProfesor}"))
                .ForMember(dest => dest.NombrePrograma,opt => opt.MapFrom(src => src.IdProgramas.FirstOrDefault().Nombre))
                .ForMember(dest => dest.EmailProfesor,opt => opt.MapFrom(src => src.IdProfesors.FirstOrDefault().Email));

            CreateMap<Materia, MateriasEstudianteDTO>()
            .ForMember(dest => dest.NombreProfesor, opt => opt.MapFrom(src => $"{src.IdProfesors.FirstOrDefault().NombresProfesor} {src.IdProfesors.FirstOrDefault().ApellidosProfesor}"))
            .ForMember(dest => dest.EmailProfesor, opt => opt.MapFrom(src => src.IdProfesors.FirstOrDefault().Email));


            //Estudiante
            CreateMap<Estudiante, EstudianteDTO>().ReverseMap();
            CreateMap<Estudiante, LoginDTO>().ReverseMap();

            // Mapeo de Estudiante a EstudianteProgramaDTO
            CreateMap<Estudiante, EstudianteProgramaDTO>()
                .ForMember(dest => dest.NombreEstudiante, opt => opt.MapFrom(src => $"{src.NombresEstudiante} {src.ApellidosEstudiante}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NombrePrograma, opt => opt.MapFrom(src => src.IdProgramaNavigation.Nombre));

            CreateMap<Estudiante, EstudianteMateriaDTO>()
                .ForMember(dest => dest.NombreEstudiante, opt => opt.MapFrom(src => $"{src.NombresEstudiante} {src.ApellidosEstudiante}"));
        }
    }
}
