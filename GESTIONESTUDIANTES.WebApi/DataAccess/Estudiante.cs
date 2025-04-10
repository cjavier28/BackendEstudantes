using System;
using System.Collections.Generic;

namespace SGEU.WebApi.DataAccess;

public partial class Estudiante
{
    public string IdEstudiante { get; set; } = null!;

    public string NombresEstudiante { get; set; } = null!;

    public string ApellidosEstudiante { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? Salt { get; set; }

    public int? IdPrograma { get; set; }

    public virtual Programa? IdProgramaNavigation { get; set; }

    public virtual ICollection<Materia> IdMateria { get; set; } = new List<Materia>();
}
