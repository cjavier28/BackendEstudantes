

namespace SGEU.WebApi.DTOS
{
    public class EstudianteDTO
    {
        public string IdEstudiante { get; set; } = null!;

        public string NombresEstudiante { get; set; } = null!;

        public string ApellidosEstudiante { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string? Salt { get; set; } = null!;

        public int? IdPrograma { get; set; } = null!;
    }
}
