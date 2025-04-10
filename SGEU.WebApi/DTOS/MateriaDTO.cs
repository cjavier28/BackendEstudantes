using SGEU.WebApi.DataAccess;

namespace SGEU.WebApi.DTOS
{
    public class MateriaDTO
    {
        public int IdMateria { get; set; }

        public string Nombre { get; set; } = null!;

        public int NumeroCreditos { get; set; }
        
    }
}
