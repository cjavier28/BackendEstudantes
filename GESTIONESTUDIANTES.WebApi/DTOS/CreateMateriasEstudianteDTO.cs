namespace SGEU.WebApi.DTOS
{
    public class CreateMateriasEstudianteDTO
    {
        public string IdEstudiante { get; set; }
        public List<int> IdMaterias { get; set; }
    }
}
