using AutoMapper;
using ServicioGestionEstudiantes.Entidades;
using ServicioGestionEstudiantes.Negocio.DTOS;

namespace ServicioGestionEstudiantes.WebApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Programa
            CreateMap<Programa, ProgramaDto>().ReverseMap();      

            //Materia
            CreateMap<Materia, MateriaDto>().ReverseMap();

            CreateMap<Materia, ProgramaMateriaDto>()
                .ForMember(dest => dest.IdPrograma, opt => opt.MapFrom(src => src!.IdProgramas!.FirstOrDefault()!.IdPrograma))
                .ForMember(dest => dest.IdProfesor,opt => opt.MapFrom(src => src!.IdProfesors!.FirstOrDefault()!.IdProfesor))
                .ForMember(dest => dest.NombreProfesor,opt => opt.MapFrom(src => $"{src!.IdProfesors!.FirstOrDefault()!.NombresProfesor} {src!.IdProfesors!.FirstOrDefault()!.ApellidosProfesor}"))
                .ForMember(dest => dest.NombrePrograma,opt => opt.MapFrom(src => src!.IdProgramas!.FirstOrDefault()!.Nombre))
                .ForMember(dest => dest.EmailProfesor,opt => opt.MapFrom(src => src!.IdProfesors!.FirstOrDefault()!.Email));

            CreateMap<Materia, MateriasEstudianteDto>()
            .ForMember(dest => dest.NombreProfesor, opt => opt.MapFrom(src => $"{src!.IdProfesors!.FirstOrDefault()!.NombresProfesor} {src.IdProfesors!.FirstOrDefault()!.ApellidosProfesor}"))
            .ForMember(dest => dest.EmailProfesor, opt => opt.MapFrom(src => src!.IdProfesors!.FirstOrDefault()!.Email));


            //Estudiante
            CreateMap<Estudiante, EstudianteDto>().ReverseMap();
            CreateMap<Estudiante, LoginDto>().ReverseMap();

            // Mapeo de Estudiante a EstudianteProgramaDTO
            CreateMap<Estudiante, EstudianteProgramaDto>()
                .ForMember(dest => dest.NombreEstudiante, opt => opt.MapFrom(src => $"{src.NombresEstudiante} {src.ApellidosEstudiante}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.NombrePrograma, opt => opt.MapFrom(src => src!.IdProgramaNavigation!.Nombre));

            CreateMap<Estudiante, EstudianteMateriaDto>()
                .ForMember(dest => dest.NombreEstudiante, opt => opt.MapFrom(src => $"{src.NombresEstudiante} {src.ApellidosEstudiante}"));
        }
    }
}
