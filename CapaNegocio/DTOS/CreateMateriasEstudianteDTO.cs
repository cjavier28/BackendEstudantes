namespace ServicioGestionEstudiantes.Negocio.DTOS
{
    public class CreateMateriasEstudianteDto
    {
        public string IdEstudiante { get; set; } =string.Empty; 
        public List<int> IdMaterias { get; set; } = new List<int>();    
    }
}
