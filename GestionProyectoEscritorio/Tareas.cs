using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectoEscritorio
{
    internal class Tareas
    {
        public string NombreTarea;
        public List<string> Subtareas { get; set; }

        public Tareas() { }
        public Tareas(string nombreTarea, List<string> subtareas)
        {
            NombreTarea = nombreTarea;
            Subtareas = subtareas;
        }
    }
}
