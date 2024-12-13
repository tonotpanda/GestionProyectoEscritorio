using System;
using System.Collections.Generic;

namespace GestionProyectoEscritorio
{
    public class Proyecto
    {
        public string NombreProyecto { get; set; }
        public List<string> Tareas { get; set; }
        public List<string> SubTareas { get; set; }
        public List<string> ListaUsuarios { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
    }
}
