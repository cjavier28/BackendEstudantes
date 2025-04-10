using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGestionEstudiantes.Entidades
{
    public class Programa
    {
        public int IdPrograma { get; set; }

        public string Nombre { get; set; } = null!;

        public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

        public virtual ICollection<Materia> IdMateria { get; set; } = new List<Materia>();
    }
}
