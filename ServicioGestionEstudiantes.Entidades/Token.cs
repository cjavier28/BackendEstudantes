using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGestionEstudiantes.Entidades
{
    public class Token
    {
        public string TokenBearer { get; set; } = string.Empty;
        public DateTime FechaHoraActual { get; set; } = DateTime.Now;
    }
}
