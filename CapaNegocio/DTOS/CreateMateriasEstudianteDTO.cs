namespace ServicioGestionEstudiantes.Negocio.DTOS
{
    public class CreateMateriasEstudianteDTO
    {
        public string IdEstudiante { get; set; } =string.Empty; 
        public List<int> IdMaterias { get; set; } = new List<int>();    
    }
}
