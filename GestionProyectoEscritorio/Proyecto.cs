using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectoEscritorio
{
    public class Proyecto
    {
        public string NombreProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<string> Usuarios { get; set; } = new List<string>();
        public string Tareas { get; set; }
        public string Subtareas { get; set; }
    }
}
