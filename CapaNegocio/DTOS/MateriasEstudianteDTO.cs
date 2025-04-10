namespace ServicioGestionEstudiantes.Negocio.DTOS
{
    public class MateriasEstudianteDto
    {
        public string IdMateria { get; set; } = null!;

        public string NombreProfesor { get; set; } = null!;

        public string EmailProfesor { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string NumeroCreditos { get; set; } = null!;

        public string NombrePrograma { get; set; } = null!;
    }
}
