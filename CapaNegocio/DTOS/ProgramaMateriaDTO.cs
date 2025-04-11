namespace ServicioGestionEstudiantes.Negocio.DTOS
{
    public class ProgramaMateriaDto
    {
        public int IdPrograma { get; set; }

        public int IdMateria { get; set; } 

        public string NombrePrograma { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string IdProfesor { get; set; } = null!;

        public string NombreProfesor{ get; set; } = null!;

        public string EmailProfesor { get; set; } = null!;

        public int NumeroCreditos { get; set; } 
    }
}
