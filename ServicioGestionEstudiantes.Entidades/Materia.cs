using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGestionEstudiantes.Entidades
{
    public class Materia
    {
        public int IdMateria { get; set; }

        public string Nombre { get; set; } = null!;

        public int NumeroCreditos { get; set; }

        public virtual ICollection<Estudiante> IdEstudiantes { get; set; } = new List<Estudiante>();

        public virtual ICollection<Profesor> IdProfesors { get; set; } = new List<Profesor>();

        public virtual ICollection<Programa> IdProgramas { get; set; } = new List<Programa>();
    }

}

