using System;
using System.Collections.Generic;

namespace SGEU.WebApi.DataAccess;

public partial class Profesor
{
    public string IdProfesor { get; set; } = null!;

    public string NombresProfesor { get; set; } = null!;

    public string ApellidosProfesor { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Materia> IdMateria { get; set; } = new List<Materia>();
}
