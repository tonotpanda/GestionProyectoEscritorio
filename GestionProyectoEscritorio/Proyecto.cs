using System;
using System.Collections.Generic;

namespace GestionProyectoEscritorio
{
    public class Proyecto
    {
        public string NombreProyecto { get; set; }
        public List<string> Tareas { get; set; }
        public DateTime FechaInicio { get; set; }  // Asegúrate de que esta propiedad sea de tipo DateTime
        public DateTime FechaFin { get; set; }  // Asegúrate de que esta propiedad sea de tipo DateTime
        public List<string> Usuarios { get; set; }
    }

}
