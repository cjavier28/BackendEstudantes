using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGestionEstudiantes.Entidades
{
    public class Profesor
    {
        public string IdProfesor { get; set; } = null!;

        public string NombresProfesor { get; set; } = null!;

        public string ApellidosProfesor { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual ICollection<Materia> IdMateria { get; set; } = new List<Materia>();
    }
}
