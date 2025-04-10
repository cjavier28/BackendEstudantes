using System;
using System.Collections.Generic;

namespace SGEU.WebApi.DataAccess;

public partial class Programa
{
    public int IdPrograma { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual ICollection<Materia> IdMateria { get; set; } = new List<Materia>();
}
